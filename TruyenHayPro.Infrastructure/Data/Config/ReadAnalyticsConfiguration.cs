using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class ReadAnalyticsConfiguration : IEntityTypeConfiguration<ReadAnalytics>
{
    public void Configure(EntityTypeBuilder<ReadAnalytics> b)
    {
        b.ToTable("ReadAnalytics");

        b.HasIndex(x => new { x.TenantId, x.Path });
    }
}
