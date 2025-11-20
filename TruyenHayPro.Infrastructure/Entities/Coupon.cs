using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class Coupon : TenantEntity
{
    [MaxLength(100)] public string Code { get; set; }
    [MaxLength(50)] public string DiscountType { get; set; } // percent/fixed
    public decimal Amount { get; set; } //số tiền chiết khấu hoặc phần trăm
    public int MaxUses { get; set; } // Số lần dùng max
    public int UsedCount { get; set; } // Số lần đã sử dụng
    public DateTime ValidFrom { get; set; } // null = ngay tao
    public DateTime ValidTo { get; set; } // null = không hết hạn
    public string AppliesTo { get; set; } // novel/chapter/plan
    public bool IsActive { get; set; } = true;
}