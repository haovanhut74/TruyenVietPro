using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities.Service;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> b)
    {
        b.ToTable("Comments");

        b.HasIndex(x => new { x.TenantId, x.NovelId });
        b.HasIndex(x => new { x.TenantId, x.ChapterId });
    }
}
