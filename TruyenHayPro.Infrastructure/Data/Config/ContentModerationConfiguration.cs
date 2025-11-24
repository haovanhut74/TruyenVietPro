using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class ContentModerationConfiguration : IEntityTypeConfiguration<ContentModeration>
{
    public void Configure(EntityTypeBuilder<ContentModeration> b)
    {
        b.ToTable("ContentModerations");

        b.HasIndex(x => new { x.TenantId, x.EntityType, x.EntityId });
    }
}
