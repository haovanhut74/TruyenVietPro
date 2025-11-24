using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

public class Report : TenantEntity
{
    public Guid ReporterUserId { get; set; }
    public string TargetType { get; set; } //Chapter/Comment/Novel.
    public Guid TargetId { get; set; }
    public string Reason { get; set; } //lý do người dùng văn bản.
    public string Status { get; set; } // Đang chờ xử lý/Đã giải quyết.
    public Guid? HandledBy { get; set; }
    public DateTime? HandledAt { get; set; }
}