using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TruyenHayPro.Application.Common;
using TruyenHayPro.Core.Entities;
using TruyenHayPro.Core.Entities.Auth;
using TruyenHayPro.Core.Entities.Base;
using TruyenHayPro.Core.Entities.Manager;
using TruyenHayPro.Core.Entities.Role;
using TruyenHayPro.Core.Entities.Service;

namespace TruyenHayPro.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly ITenantProvider _tenantProvider;
        private readonly Guid? _currentTenantId;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider = null,
            ILogger<AppDbContext> logger = null)
            : base(options)
        {
            _tenantProvider = tenantProvider;
            _currentTenantId = tenantProvider?.GetCurrentTenantId();
            _logger = logger;
        }

        // DbSets
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Novel> Novels { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<AuthorProfile> AuthorProfiles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NovelTag> NovelTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NovelCategory> NovelCategories { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<ReadProgress> ReadProgresses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<MediaAsset> MediaAssets { get; set; }

        public DbSet<SeoMetadata> SeoMetadatas { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ContentModeration> ContentModerations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<FailedJob> FailedJobs { get; set; }
        public DbSet<ReadAnalytics> ReadAnalytics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply IEntityTypeConfiguration<> implementations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // SQL Server specific: default GUID generation using NEWID()
            if (Database.IsSqlServer())
            {
                modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(Guid) && p.Name == "Id")
                    .ToList()
                    .ForEach(p => p.SetDefaultValueSql("NEWID()"));
            }
            else
            {
                // Fallback: client-side GUID generation
                modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(Guid) && p.Name == "Id")
                    .ToList()
                    .ForEach(p => p.ValueGenerated = ValueGenerated.OnAdd);
            }

            // Apply global filters (soft-delete + tenant if available)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (clrType == null) continue;

                if (typeof(AuditedEntity).IsAssignableFrom(clrType))
                {
                    var method = typeof(AppDbContext)
                        .GetMethod(nameof(ApplyGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method == null) continue;
                    var generic = method.MakeGenericMethod(clrType);
                    generic.Invoke(this, new object[] { modelBuilder });
                }
            }

            // Useful indexes
            modelBuilder.Entity<Chapter>().HasIndex(c => c.NovelId);
            modelBuilder.Entity<Rating>().HasIndex(r => new { r.UserId, r.NovelId }).IsUnique(false);

            base.OnModelCreating(modelBuilder);
        }

        // Compose lambda: e => !e.IsDeleted && (Tenant filter if applicable)
        private void ApplyGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : AuditedEntity
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var isDeletedExpr = Expression.Property(parameter, nameof(AuditedEntity.IsDeleted));
            var notDeletedExpr = Expression.Equal(isDeletedExpr, Expression.Constant(false));

            Expression final = notDeletedExpr;

            if (typeof(TenantEntity).IsAssignableFrom(typeof(TEntity)))
            {
                if (_currentTenantId.HasValue)
                {
                    var tenantProp = Expression.Property(parameter, nameof(TenantEntity.TenantId));
                    var tenantEquals = Expression.Equal(tenantProp, Expression.Constant(_currentTenantId.Value));
                    final = Expression.AndAlso(notDeletedExpr, tenantEquals);
                }
                else
                {
                    _logger?.LogDebug("No tenant id available; Tenant filter skipped for {Entity}.",
                        typeof(TEntity).Name);
                }
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(final, parameter);
            modelBuilder.Entity<TEntity>().HasQueryFilter(lambda);
        }

        public override int SaveChanges()
        {
            HandleAuditAndSoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleAuditAndSoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleAuditAndSoftDelete()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is AuditedEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = (AuditedEntity)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTime.UtcNow;
                        if (entity is TenantEntity tenantEntity && tenantEntity.TenantId == Guid.Empty &&
                            _currentTenantId.HasValue)
                        {
                            tenantEntity.TenantId = _currentTenantId.Value;
                        }

                        break;
                    case EntityState.Modified:
                        entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        // convert physical delete -> soft delete
                        entry.State = EntityState.Modified;
                        entity.IsDeleted = true;
                        entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }
        }
    }

    public static class DbContextServiceCollectionExtensions
    {
        public static void AddTruyenHayProDbContext(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                options.UseSqlServer(connectionString,
                    sql =>
                    {
                        sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                var env = provider.GetService<IHostEnvironment>();
                var loggerFactory = provider.GetService<ILoggerFactory>();
                options.UseLoggerFactory(loggerFactory);
                if (env != null && env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });
        }
    }
}