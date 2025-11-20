using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Role;

public class RolePermission : AuditedEntity
{
    [Required] public Guid RoleId { get; set; }
    public Role Role { get; set; }

    [Required] public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}