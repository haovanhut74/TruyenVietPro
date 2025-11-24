using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data;

public class NovelConfiguration : IEntityTypeConfiguration<Novel>
{
    public void Configure(EntityTypeBuilder<Novel> builder)
    {
        builder.ToTable("Novels");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title).IsRequired().HasMaxLength(300);
        builder.Property(n => n.Slug).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Language).HasMaxLength(10).HasDefaultValue("vi");

        builder.HasIndex(n => new { n.TenantId, n.Slug }).IsUnique();
        builder.HasIndex(n => new { n.TenantId, n.PublishedAt });
        builder.HasMany(n => n.Chapters)
            .WithOne(c => c.Novel)
            .HasForeignKey(c => c.NovelId)
            .OnDelete(DeleteBehavior.Cascade); // consider soft-delete cascade in app instead

        // Cover frequently read fields as included columns for SQL Server covering index if needed:
        // builder.HasIndex(n => new { n.TenantId, n.Status }).IncludeProperties(n => new { n.Title, n.CoverUrl });
    }
}
