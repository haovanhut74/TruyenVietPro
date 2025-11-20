using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Auth;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> b)
    {
        b.ToTable("RefreshTokens");

        b.HasIndex(x => new { x.UserId, x.TokenHash }).IsUnique();
    }
}
