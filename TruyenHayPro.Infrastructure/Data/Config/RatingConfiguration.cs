using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Service;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> b)
    {
        b.ToTable("Ratings");

        b.HasIndex(x => new { x.TenantId, x.UserId, x.NovelId }).IsUnique();
    }
}
