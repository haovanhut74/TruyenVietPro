using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Role;

public class Role : TenantEntity
{
    [Required, MaxLength(100)] public string Name { get; set; } //tên role 

    [Required, MaxLength(100)] public string NormalizedName { get; set; } //chuẩn hóa để lookup.

    [MaxLength(500)] public string Description { get; set; } //mô tả.

    public bool IsSystem { get; set; } = false; //nếu role hệ thống (không xóa).

    public ICollection<RolePermission> RolePermissions { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}