using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Auth;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Role;

public class UserRole : AuditedEntity
{
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public DateTime AssignedAt { get; set; } //thời điểm gán.
}