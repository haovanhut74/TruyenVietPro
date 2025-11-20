using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class Bookmark : TenantEntity
{
    public Guid UserId { get; set; } // ai đã đánh dấu trang
    public Guid NovelId { get; set; } //tiểu thuyết nào
    public Guid? ChapterId { get; set; } //chương cụ thể tùy chọn
    public DateTime AddedAt { get; set; } // ngày thêm
}