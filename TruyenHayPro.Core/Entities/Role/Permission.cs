using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Role;

public class Permission : AuditedEntity
{
    [Required, MaxLength(150)]
    public string Code { get; set; } // mã quyền cố định (ví dụ novel.publish). Giữ ổn định (không đổi).

    [MaxLength(200)] public string Name { get; set; } //tên hiển thị.

    [MaxLength(500)] public string Description { get; set; } //chú thích.
}