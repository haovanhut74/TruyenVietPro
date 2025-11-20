using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Auth;

public class RefreshToken : AuditedEntity
{
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }
    [MaxLength(200)] public string TokenHash { get; set; } // hash token
    public DateTime ExpiresAt { get; set; } //hết hạn
    public DateTime? RevokedAt { get; set; } //thời gian thu hồi.
    public string CreatedByIp { get; set; }
    public string RevokedByIp { get; set; }
    public string ReplacedByToken { get; set; }
}