using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Auth;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Role;

public class UserRole : AuditedEntity
{
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    public Core.Entities.Role.Role Role { get; set; }

    public DateTime AssignedAt { get; set; } //thời điểm gán.
}