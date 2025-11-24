using System.ComponentModel.DataAnnotations;

namespace TruyenHayPro.Core.Entities.Base;

/// Các trường kiểm tra chung được hầu hết mọi bảng sử dụng.
/// Dấu thời gian được lưu trữ theo UTC (datetime2).
public abstract class AuditedEntity
{
    [Key] public Guid Id { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public Guid? CreatedById { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedById { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}