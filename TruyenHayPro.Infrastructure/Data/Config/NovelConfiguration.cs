using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class NovelConfiguration : IEntityTypeConfiguration<Novel>
{
    public void Configure(EntityTypeBuilder<Novel> b)
    {
        b.ToTable("Novels");

        b.Property(x => x.Title).IsRequired().HasMaxLength(300);
        b.Property(x => x.Slug).IsRequired().HasMaxLength(200);

        b.HasIndex(x => new { x.TenantId, x.Slug }).IsUnique();
        b.HasIndex(x => new { x.TenantId, x.PublishedAt });

        b.HasMany(x => x.Chapters)
            .WithOne(x => x.Novel)
            .HasForeignKey(x => x.NovelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}