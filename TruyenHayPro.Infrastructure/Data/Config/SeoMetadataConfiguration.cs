using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class SeoMetadataConfiguration : IEntityTypeConfiguration<SeoMetadata>
{
    public void Configure(EntityTypeBuilder<SeoMetadata> b)
    {
        b.ToTable("SeoMetadatas");

        b.HasIndex(x => new { x.EntityType, x.EntityId }).IsUnique(false);
    }
}
