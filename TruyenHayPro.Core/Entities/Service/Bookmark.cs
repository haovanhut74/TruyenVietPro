using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Service;

public class Bookmark : TenantEntity
{
    public Guid UserId { get; set; } // ai đã đánh dấu trang
    public Guid NovelId { get; set; } //tiểu thuyết nào
    public Guid? ChapterId { get; set; } //chương cụ thể tùy chọn
    public DateTime AddedAt { get; set; } // ngày thêm
}