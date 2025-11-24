using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Service;

public class Rating : TenantEntity
{
    public Guid UserId { get; set; }
    public Guid NovelId { get; set; }

    public byte Score { get; set; } // 1..5

    [MaxLength(200)]
    public string Title { get; set; } //tiêu đề review.

    public string Review { get; set; } //nội dung review.

    public bool IsVerifiedPurchase { get; set; } = false; //chứng thực.
}