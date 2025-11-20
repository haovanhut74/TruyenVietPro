using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> b)
    {
        b.ToTable("Chapters");

        b.Property(x => x.Title).IsRequired().HasMaxLength(300);
        b.Property(x => x.Slug).HasMaxLength(300);

        b.HasIndex(x => new { x.NovelId, x.Number }).IsUnique();
        b.HasIndex(x => new { x.NovelId, x.Slug }).IsUnique(false);
    }
}