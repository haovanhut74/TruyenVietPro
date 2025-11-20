using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Role;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> b)
    {
        b.ToTable("UserRoles");

        b.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
    }
}