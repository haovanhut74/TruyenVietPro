using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Service;

public class ReadProgress : TenantEntity
{
    public Guid UserId { get; set; }
    public Guid NovelId { get; set; }
    public Guid ChapterId { get; set; }
    public int Position { get; set; } // offset (chỉ mục ký tự/đoạn văn)
    public DateTime UpdatedAt { get; set; } // đồng bộ lần cuối.
}