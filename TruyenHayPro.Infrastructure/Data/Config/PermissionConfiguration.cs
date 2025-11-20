using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Role;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> b)
    {
        b.ToTable("Permissions");

        b.HasIndex(x => x.Code).IsUnique();
        b.Property(x => x.Code).HasMaxLength(200).IsRequired();
    }
}