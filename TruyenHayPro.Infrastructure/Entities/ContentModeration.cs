using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class ContentModeration : TenantEntity
{
    public string EntityType { get; set; } //Novel/Chapter/Comment...
    public Guid EntityId { get; set; }
    public string ReasonCode { get; set; } // ví dụ, tình dục, thù hận.
    public double Score { get; set; } //điểm số mô hình.
    public DateTime DetectedAt { get; set; } //time detected.
    public string ActionTaken { get; set; } //tự động ẩn/đánh dấu/xóa.
    public Guid? ModeratorId { get; set; } //người kiểm duyệt nếu được xem xét.
}