using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Role;

public class RolePermission : AuditedEntity
{
    [Required] public Guid RoleId { get; set; }
    public Core.Entities.Role.Role Role { get; set; }

    [Required] public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}