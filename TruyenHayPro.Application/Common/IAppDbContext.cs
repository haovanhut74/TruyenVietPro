using Microsoft.EntityFrameworkCore;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Application.Common;

public interface IAppDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<NovelCategory> NovelCategories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}