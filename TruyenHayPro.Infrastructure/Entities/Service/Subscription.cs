using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Service;

public class Subscription : TenantEntity
{
    public Guid UserId { get; set; }
    public Guid PlanId { get; set; }
    public DateTime StartAt { get; set; } // Ngày bắt đầu
    public DateTime? EndAt { get; set; } // Ngày kết thúc
    [MaxLength(50)] public string Status { get; set; } //Đang hoạt động/Đã hủy/Quá hạn
    public bool AutoRenew { get; set; } = true;
    [MaxLength(200)] public string BillingProviderCustomerId { get; set; } //nhà cung cấp khách hàng id
}