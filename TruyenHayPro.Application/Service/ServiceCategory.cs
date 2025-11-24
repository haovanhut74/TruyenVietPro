using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using TruyenHayPro.Application.Common;
using TruyenHayPro.Application.Contracts.Category;
using TruyenHayPro.Application.DTO.Admin;
using TruyenHayPro.Application.Interfaces.Repository;
using TruyenHayPro.Application.Interfaces.Services;

namespace TruyenHayPro.Application.Service;

public partial class ServiceCategory : IServiceCategory
{
    private readonly IRepoCategory _repo;

    public ServiceCategory(IRepoCategory repo)
    {
        _repo = repo;
    }

    // Danh sách Admin + phân trang
// lấy danh sách cho admin với phân trang, nhận CategoryListQuery để tái sử dụng
    public async Task<PagedResult<CategoryAdminDto>> GetAdminListAsync(
        CategoryListQuery query,
        CancellationToken ct = default)
    {
        // chuẩn hoá paging
        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 200); // giới hạn tối đa 200

        // chuẩn hoá từ khoá tìm kiếm
        var keyword = string.IsNullOrWhiteSpace(query.Q) ? null : query.Q.Trim();

        // lấy source projection (IQueryable<CategoryAdminDto>)
        var source = _repo.QueryAsAdmin();

        // lọc theo tên nếu có tìm kiếm
        if (!string.IsNullOrEmpty(keyword))
        {
            var s = keyword.ToLowerInvariant();
            // dùng EF.Functions.Like để chuyển xuống SQL, chuyển tên về lower trong expression để đảm bảo translate
            source = source.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{s}%"));
        }

        // tổng số trước khi phân trang
        var total = await source.CountAsync(ct);
        if (total == 0)
            return new PagedResult<CategoryAdminDto>(new List<CategoryAdminDto>(), 0, page, pageSize);

        // lấy trang hiện tại
        var items = await source
            .OrderBy(x => x.OrderIndex)
            .ThenBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<CategoryAdminDto>(items, total, page, pageSize);
    }


    // Chi tiết Admin
    public async Task<CategoryAdminDto?> GetAdminDetailAsync(Guid id, CancellationToken ct = default)
    {
        return await _repo.QueryAsAdmin()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    // Tạo mới Category
    public async Task<Guid> CreateAsync(CategoryCreateRequest request, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Tên không được để trống");

        if (request.Name.Length > 200)
            throw new ArgumentException("Tên quá dài (tối đa 200 ký tự)");

        var normalizedSlug = NormalizeSlug(request.Slug ?? request.Name);

        if (await _repo.ExistsBySlugAsync(normalizedSlug, ct))
            throw new ArgumentException("Slug đã tồn tại");

        // Check ParentCategory
        if (request.ParentCategoryId.HasValue)
        {
            var parent = await _repo.GetByIdAsync(request.ParentCategoryId.Value, ct);
            if (parent == null)
                throw new ArgumentException("Category cha không tồn tại");

            if (parent.IsDeleted)
                throw new ArgumentException("Category cha đã bị xóa");
        }

        var entity = new Core.Entities.Category
        {
            Name = request.Name.Trim(),
            Slug = normalizedSlug,
            ParentCategoryId = request.ParentCategoryId,
            OrderIndex = request.OrderIndex
        };

        await _repo.AddAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);

        return entity.Id;
    }

    // Cập nhật Category
    public async Task<bool> UpdateAsync(Guid id, CategoryUpdateRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity == null)
            return false;

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Tên không được để trống");

        if (request.Name.Length > 200)
            throw new ArgumentException("Tên quá dài");

        var newSlug = NormalizeSlug(request.Slug ?? entity.Slug);

        // // Check slug conflict
        // var slugConflict = await _repo.ExistsBySlugExceptIdAsync(newSlug, id, ct);
        // if (slugConflict)
        //     throw new ArgumentException("Slug đã tồn tại");

        // Prevent Self-loop
        if (request.ParentCategoryId == id)
            throw new ArgumentException("Category không thể là cha của chính nó");

        // Validate Parent
        if (request.ParentCategoryId.HasValue)
        {
            var parent = await _repo.GetByIdAsync(request.ParentCategoryId.Value, ct);
            if (parent == null || parent.IsDeleted)
                throw new ArgumentException("Category cha không hợp lệ");
        }

        // Early skip if no change
        if (entity.Name == request.Name &&
            entity.Slug == newSlug &&
            entity.ParentCategoryId == request.ParentCategoryId &&
            entity.OrderIndex == request.OrderIndex)
        {
            return true;
        }

        entity.Name = request.Name.Trim();
        entity.Slug = newSlug;
        entity.ParentCategoryId = request.ParentCategoryId;
        entity.OrderIndex = request.OrderIndex;

        _repo.Update(entity);
        await _repo.SaveChangesAsync(ct);

        return true;
    }

    // Xóa Category
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity == null)
            return false;

        // Prevent delete when in use
        // if (entity.NovelCategories?.Count > 0)
        //     throw new InvalidOperationException("Không thể xóa Category đang được sử dụng.");

        _repo.SoftDelete(entity);
        await _repo.SaveChangesAsync(ct);
        return true;
    }

    // Chuẩn hóa slug
    private static string NormalizeSlug(string input)
    {
        var slug = input.Trim().ToLowerInvariant();
        slug = MyRegex().Replace(slug, "-");
        slug = MyRegex1().Replace(slug, "");
        if (slug.Length > 100) slug = slug[..100];
        return slug;
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex MyRegex();

    [GeneratedRegex(@"[^a-z0-9\-]")]
    private static partial Regex MyRegex1();
}