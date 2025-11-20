using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
{
    public void Configure(EntityTypeBuilder<SystemSetting> b)
    {
        b.ToTable("SystemSettings");

        b.HasIndex(x => new { x.TenantId, x.Key }).IsUnique(false);
    }
}
