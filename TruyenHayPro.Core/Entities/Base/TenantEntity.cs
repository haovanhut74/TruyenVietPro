using System.ComponentModel.DataAnnotations;

namespace TruyenHayPro.Core.Entities.Base;

/// Các thực thể thuộc về đối tượng thuê (phương pháp DB dùng chung).
/// Nếu sử dụng schema-per-tenant, TenantId có thể là null hoặc không được sử dụng.
public abstract class TenantEntity : AuditedEntity
{
    [Required] public Guid TenantId { get; set; }
}