using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities.Auth;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("Users");

        b.HasIndex(x => new { x.TenantId, x.EmailNormalized }).IsUnique();
        b.HasIndex(x => x.Username);

        b.Property(x => x.Email).HasMaxLength(256);
        b.Property(x => x.EmailNormalized).HasMaxLength(256);
        b.Property(x => x.Username).HasMaxLength(100);

        b.HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}