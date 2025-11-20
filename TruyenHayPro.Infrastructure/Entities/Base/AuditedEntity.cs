using System.ComponentModel.DataAnnotations;

namespace TruyenHayPro.Infrastructure.Entities.Base;

/// Các trường kiểm tra chung được hầu hết mọi bảng sử dụng.
/// Dấu thời gian được lưu trữ theo UTC (datetime2).
public abstract class AuditedEntity
{
    [Key] public Guid Id { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedById { get; set; }

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedById { get; set; }
    [Required] public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;
}