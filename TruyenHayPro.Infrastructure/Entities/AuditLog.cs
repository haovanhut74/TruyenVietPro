using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Auth;

// 21. AuditLog / ActivityLog (immutable append-only)
public class AuditLog : AuditedEntity
{
    public Guid? ActorUserId { get; set; }
    [MaxLength(200)] public string Action { get; set; } //e.g., PublishChapter.
    [MaxLength(100)] public string TargetType { get; set; } // Chapter/Novel/User.
    public Guid? TargetId { get; set; }
    public string DetailsJson { get; set; } // chi tiết không thay đổi (bối cảnh).
}