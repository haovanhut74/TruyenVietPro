using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

// 29. FailedJob
public class FailedJob : AuditedEntity
{
    [MaxLength(200)] public string JobType { get; set; } // 
    public string PayloadJson { get; set; } //tải trọng công việc
    public string Status { get; set; } //Đang chờ xử lý/Không thành công/Đã hoàn thành.
    public int AttemptCount { get; set; } ////số lần thử.
    public string LastError { get; set; } //lỗi mới nhất.
    public DateTime? NextAttemptAt { get; set; } //scheduled retry.
}