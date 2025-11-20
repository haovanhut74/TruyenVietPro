using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> b)
    {
        b.ToTable("Reports");

        b.HasIndex(x => new { x.TenantId, x.TargetType, x.TargetId });
    }
}
