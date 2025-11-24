using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data;


public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.ToTable("Chapters");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Slug).HasMaxLength(300);
        builder.HasIndex(c => new { c.NovelId, c.Number }).IsUnique();
        builder.HasIndex(c => new { c.NovelId, c.Slug }).IsUnique(false);

        // For long content prefer storing externally and reference ContentUrl
    }
}