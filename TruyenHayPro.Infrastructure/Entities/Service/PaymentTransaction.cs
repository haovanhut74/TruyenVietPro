using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class PaymentTransaction : TenantEntity
{
    public Guid? UserId { get; set; }
    public Guid? OrderId { get; set; }

    public decimal Amount { get; set; }
    [MaxLength(10)] public string Currency { get; set; } = "VND";

    [MaxLength(100)] public string Provider { get; set; } // Stripe/PayPal/VNPay

    //ID giao dịch của nhà cung cấp; duy nhất cho mỗi nhà cung cấp
    [MaxLength(200)] public string ProviderTransactionId { get; set; }

    [MaxLength(50)] public string Status { get; set; } // Đang chờ xử lý/Thành công/Thất bại/Đã hoàn tiền

    public string MetadataJson { get; set; } //tải trọng webhook của nhà cung cấp tối thiểu.
    public DateTime? ProcessedAt { get; set; }
}