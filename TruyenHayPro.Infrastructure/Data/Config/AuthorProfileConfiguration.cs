using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Auth;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class AuthorProfileConfiguration : IEntityTypeConfiguration<AuthorProfile>
{
    public void Configure(EntityTypeBuilder<AuthorProfile> b)
    {
        b.ToTable("AuthorProfiles");

        b.Property(x => x.DisplayName).HasMaxLength(200);

        b.HasIndex(x => new { x.TenantId, x.UserId }).IsUnique();
    }
}