using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Core.Entities.Service;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> b)
    {
        b.ToTable("Bookmarks");

        b.HasIndex(x => new { x.TenantId, x.UserId, x.NovelId });
    }
}
