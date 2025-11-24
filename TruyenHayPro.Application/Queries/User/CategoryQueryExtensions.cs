using TruyenHayPro.Application.DTO.Admin;
using TruyenHayPro.Application.DTO.User;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Application.Queries.User;

public static class CategoryQueryExtensions
{
    // Simple projection (safe, no joins)
    extension(IQueryable<Category> q)
    {
        public IQueryable<CategoryDto> ProjectToCategoryDto()
        {
            return q
                .Where(c => !c.IsDeleted)
                .Select(c => new CategoryDto(
                    c.Id,
                    c.Name,
                    c.Slug,
                    c.ParentCategoryId,
                    c.OrderIndex
                ));
        }

        public IQueryable<CategoryAdminDto> ProjectToCategoryAdminDto(IQueryable<NovelCategory> novelCategories)
        {
            return q
                .Where(c => !c.IsDeleted)
                .Select(c => new CategoryAdminDto(
                    c.Id,
                    c.TenantId,
                    c.Name,
                    c.Slug,
                    c.ParentCategoryId,

                    // ParentCategoryName via correlated subquery
                    q
                        .Where(p => p.Id == c.ParentCategoryId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    c.OrderIndex,
                    c.IsDeleted,
                    c.CreatedAt,
                    c.CreatedById,
                    c.UpdatedAt,
                    c.UpdatedById,

                    // NovelCount via correlated subquery on NovelCategories
                    novelCategories.Count(nc => nc.CategoryId == c.Id)
                ));
        }
    }

    // Admin projection: uses correlated subqueries for ParentName and NovelCount.
    // Pass in novelCategories (DbSet<NovelCategory>) from your DbContext when calling.
}