using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities.Manager;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(EntityTypeBuilder<MediaAsset> b)
    {
        b.ToTable("MediaAssets");

        b.HasIndex(x => new { x.TenantId, x.OwnerId });
    }
}
