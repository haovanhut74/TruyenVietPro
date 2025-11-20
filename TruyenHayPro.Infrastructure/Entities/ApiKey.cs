using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

// 23. ApiKey (partner integrations)
public class ApiKey : TenantEntity
{
    [MaxLength(100)] public string KeyId { get; set; } // ID công khai được hiển thị cho khách hàng.

    [MaxLength(200)]
    // chỉ lưu trữ hàm băm (HMAC-SHA256), không bao giờ lưu trữ văn bản thuần túy.
    public string SecretHash { get; set; }

    public string Description { get; set; } // Mô tả
    public string AllowedIps { get; set; } //danh sách trắng.
    public string ScopesJson { get; set; } //điểm cuối được phép
    public bool IsActive { get; set; } = true;
}