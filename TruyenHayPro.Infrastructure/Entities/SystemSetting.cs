using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

// 28. SystemSetting
public class SystemSetting : AuditedEntity
{
    public Guid? TenantId { get; set; } // null => global
    [MaxLength(200)] public string Key { get; set; } //cài đặt phím
    public string ValueJson { get; set; } //giá trị json.
}