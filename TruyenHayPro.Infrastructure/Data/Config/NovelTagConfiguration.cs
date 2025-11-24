using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class NovelTagConfiguration : IEntityTypeConfiguration<NovelTag>
{
    public void Configure(EntityTypeBuilder<NovelTag> b)
    {
        b.ToTable("NovelTags");
        b.HasIndex(x => new { x.NovelId, x.TagId }).IsUnique();
    }
}