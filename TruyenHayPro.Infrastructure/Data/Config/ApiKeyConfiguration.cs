using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> b)
    {
        b.ToTable("ApiKeys");

        b.HasIndex(x => new { x.TenantId, x.KeyId }).IsUnique();
    }
}
