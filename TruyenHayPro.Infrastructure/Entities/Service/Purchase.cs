using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Service;

// 15. Purchase / Entitlement
public class Purchase : TenantEntity
{
    public Guid UserId { get; set; }
    public Guid? NovelId { get; set; }
    public Guid? ChapterId { get; set; }
    public Guid? TransactionId { get; set; } // FK → PaymentTransaction.
    public DateTime GrantedAt { get; set; } //thời gian truy cập được cấp.
    public DateTime? ExpiresAt { get; set; } //nếu thời gian có hạn.
    [MaxLength(50)] public string AccessType { get; set; } //chương đơn/đăng ký/gói.
}