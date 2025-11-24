using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities.Role;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> b)
    {
        b.ToTable("RolePermissions");

        b.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();
    }
}