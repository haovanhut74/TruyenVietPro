using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Service;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> b)
    {
        b.ToTable("Purchases");

        b.HasIndex(x => new { x.TenantId, x.UserId });
    }
}