using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities.Service;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class ReadProgressConfiguration : IEntityTypeConfiguration<ReadProgress>
{
    public void Configure(EntityTypeBuilder<ReadProgress> b)
    {
        b.ToTable("ReadProgresses");

        b.HasIndex(x => new { x.TenantId, x.UserId, x.NovelId });
    }
}
