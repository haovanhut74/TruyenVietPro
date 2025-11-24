using Microsoft.EntityFrameworkCore;
using TruyenHayPro.Application.Common;
using TruyenHayPro.Application.DTO.Admin;
using TruyenHayPro.Application.Queries.Admin;
using TruyenHayPro.Application.Queries.User;

namespace TruyenHayPro.Application.Handler;

public class CategoryAdminListHandler
{
    private readonly IAppDbContext _db;

    public CategoryAdminListHandler(IAppDbContext db)
    {
        _db = db;
    }


    public async Task<PagedResult<CategoryAdminDto>> HandleAsync(
        CategoryAdminListQuery query,
        CancellationToken ct = default)
    {
        var baseQuery = _db.Categories.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            baseQuery = baseQuery.Where(c =>
                c.Name.Contains(query.Search) ||
                c.Slug.Contains(query.Search));
        }

        var projected = baseQuery.ProjectToCategoryAdminDto(_db.NovelCategories);

        var total = await projected.CountAsync(ct);

        var items = await projected
            .OrderByDescending(x => x.CreatedAt)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<CategoryAdminDto>(
            items,
            total,
            query.Page,
            query.PageSize
        );
    }
}