using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class NovelCategoryConfiguration : IEntityTypeConfiguration<NovelCategory>
{
    public void Configure(EntityTypeBuilder<NovelCategory> b)
    {
        b.ToTable("NovelCategories");

        b.HasIndex(x => new { x.NovelId, x.CategoryId }).IsUnique();
    }
}
