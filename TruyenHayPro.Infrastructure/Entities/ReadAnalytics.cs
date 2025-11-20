using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class ReadAnalytics : AuditedEntity
{
    public Guid? TenantId { get; set; }
    public string Path { get; set; } //tuyến đường/đường dẫn chuẩn.
    public Guid? UserId { get; set; }
    public string IpHash { get; set; } // hashed IP
    public string UserAgentHash { get; set; }
    public string Referer { get; set; } //từ đâu đến.
    public string Country { get; set; } //tra cứu địa lý (có thể là null).
    public string EventType { get; set; } // view/click
}