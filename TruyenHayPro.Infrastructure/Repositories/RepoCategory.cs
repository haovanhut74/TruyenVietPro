// File: /src/TruyenHayPro.Infrastructure/Repositories/RepoCategory.cs

using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using TruyenHayPro.Application.Common;
using TruyenHayPro.Core.Entities;
using TruyenHayPro.Application.DTO.Admin;
using TruyenHayPro.Application.DTO.User;
using TruyenHayPro.Application.Interfaces.Repository;
using TruyenHayPro.Application.Queries.User; // để dùng extension ProjectToCategoryDto / AdminDto
using TruyenHayPro.Infrastructure.Data; // AppDbContext

namespace TruyenHayPro.Infrastructure.Repositories
{
    // RepoCategory: implementation của IRepoCategory (nằm trong Infrastructure)
    // - Tối ưu: compiled queries cho những thao tác hot
    // - Bảo mật: public/query admin tách bạch, không lộ dữ liệu nhạy cảm cho public
    // - Cache: materialized public list caching qua IDistributedCache (Redis)
    public class RepoCategory : IRepoCategory
    {
        // IAppDbContext để giữ Clean Architecture
        private readonly IAppDbContext _db;
        private readonly IDistributedCache _cache;
        private readonly ILogger<RepoCategory> _logger;

        // TTL cache công khai (tùy chỉnh)
        private readonly TimeSpan _publicCacheTtl = TimeSpan.FromSeconds(45);

        // Prefix cache key (nhớ kèm tenant khi tạo key)
        private const string PublicListCachePrefix = "public:categories:";

        // Compiled queries: dùng AppDbContext làm TContext (EF requirement)
        // - GetById compiled
        private static readonly Func<AppDbContext, Guid, CancellationToken, Task<Category?>> CompiledGetById =
            EF.CompileAsyncQuery((AppDbContext ctx, Guid id, CancellationToken ct) =>
                ctx.Categories.Where(c => c.Id == id).FirstOrDefault());

        // - ExistsBySlug compiled
        private static readonly Func<AppDbContext, string, CancellationToken, Task<bool>> CompiledExistsBySlug =
            EF.CompileAsyncQuery((AppDbContext ctx, string slug, CancellationToken ct) =>
                ctx.Categories.Any(c => c.Slug == slug));

        public RepoCategory(IAppDbContext db, IDistributedCache cache, ILogger<RepoCategory> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Lấy Category theo Id
        // - Dùng compiled query (cast IAppDbContext -> AppDbContext)
        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            // cast an toàn: infrastructure đã đăng ký AppDbContext implement IAppDbContext
            if (_db is not AppDbContext appCtx)
            {
                // fallback: không dùng compiled query nếu không thể cast
                return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
            }

            return await CompiledGetById(appCtx, id, ct);
        }

        // Trả IQueryable<CategoryDto> cho public (projection nhẹ)
        // Lưu ý: không cache IQueryable; cache materialized list tại service/controller nếu cần
        public IQueryable<CategoryDto> QueryAsPublic()
        {
            return _db.Categories
                .AsNoTracking()
                .ProjectToCategoryDto();
        }

        // Trả IQueryable<CategoryAdminDto> cho Admin (projection đầy đủ)
        public IQueryable<CategoryAdminDto> QueryAsAdmin()
        {
            return _db.Categories
                .AsNoTracking()
                .ProjectToCategoryAdminDto(_db.NovelCategories);
        }

        // Thêm Category mới (không commit)
        public async Task AddAsync(Category entity, CancellationToken ct = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Chuẩn hoá slug nếu cần
            if (string.IsNullOrWhiteSpace(entity.Slug))
            {
                entity.Slug = GenerateSlug(entity.Name);
            }

            // Validate cơ bản
            if (entity.Name.Length > 200)
                throw new ArgumentException("Name quá dài", nameof(entity.Name));

            await _db.Categories.AddAsync(entity, ct);
        }

        // Đánh dấu update (caller sẽ SaveChanges)
        public void Update(Category entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _db.Categories.Update(entity);
        }

        // Soft-delete: set IsDeleted = true (global filter sẽ ẩn)
        public void SoftDelete(Category entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
            _db.Categories.Update(entity);
        }

        // Kiểm tra tồn tại theo Id - nhanh
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Categories.AnyAsync(c => c.Id == id, ct);
        }

        // Kiểm tra tồn tại theo Slug - dùng compiled query nếu có AppDbContext
        public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(slug)) return false;

            if (_db is AppDbContext appCtx)
            {
                return await CompiledExistsBySlug(appCtx, slug, ct);
            }

            return await _db.Categories.AnyAsync(c => c.Slug == slug, ct);
        }

        // Lưu thay đổi - sau khi commit cố gắng invalidate cache liên quan (best effort)
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            // Trước khi SaveChanges: có thể thu thập thông tin outbox/audit nếu cần (không implement ở đây)
            var changed = await _db.SaveChangesAsync(ct);

            // Sau commit: invalidate public list cache (simple strategy)
            try
            {
                await InvalidatePublicListCacheAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể invalidate public category cache");
            }

            return changed;
        }

        // -------------------------
        // Helper: cache materialized public list (service có thể gọi)
        // - materializeFunc: cách lấy dữ liệu thực tế (ví dụ: query.Skip(...).Take(...).ToListAsync())
        // - cacheKey: phải kèm tenantId + q + page để tránh leak multi-tenant
        public async Task<List<CategoryDto>> GetPublicListCachedAsync(string cacheKey,
            Func<Task<List<CategoryDto>>> materializeFunc, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) throw new ArgumentNullException(nameof(cacheKey));
            if (materializeFunc == null) throw new ArgumentNullException(nameof(materializeFunc));

            try
            {
                var cached = await _cache.GetAsync(cacheKey, ct);
                if (cached != null)
                {
                    var json = Encoding.UTF8.GetString(cached);
                    var items = JsonSerializer.Deserialize<List<CategoryDto>>(json);
                    if (items != null) return items;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Lỗi khi đọc cache public categories {Key}", cacheKey);
            }

            // Cache miss: materialize
            var result = await materializeFunc();

            try
            {
                var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _publicCacheTtl
                };
                await _cache.SetAsync(cacheKey, bytes, options, ct);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Lỗi khi set cache public categories {Key}", cacheKey);
            }

            return result;
        }

        // Invalidate public list cache
        // Note: IDistributedCache không hỗ trợ list keys - production nên dùng Redis + pub/sub hoặc maintain key registry
        private async Task InvalidatePublicListCacheAsync(CancellationToken ct = default)
        {
            try
            {
                // Minimal approach: publish a simple marker into a known cache key to indicate invalidation.
                // Consumers can check this marker or subscribe to Redis channel to invalidate local caches.
                // Nếu anh dùng Redis và muốn, em có thể implement pub/sub via StackExchange.Redis IConnectionMultiplexer.
                var markerKey = PublicListCachePrefix + "invalidate_marker";
                var payload = Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString("o"));
                await _cache.SetAsync(markerKey, payload, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                }, ct);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể set invalidate marker");
            }
        }

        // Utility: generate simple slug (tùy anh có thể thay bằng util chung)
        private static string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return Guid.NewGuid().ToString();

            var slug = name.Trim().ToLowerInvariant();
            // thay khoảng trắng bằng dấu '-'
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");
            // loại bỏ ký tự không phải a-z0-9-
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
            // crop length
            if (slug.Length > 100) slug = slug.Substring(0, 100);
            return slug;
        }
    }
}