using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;
using TruyenHayPro.Core.Entities.Role;

namespace TruyenHayPro.Core.Entities.Auth;

public class Tenant : AuditedEntity
{
    [Required, MaxLength(100)] public string Slug { get; set; } //Tên ngắn, dùng URL/lookup (ví dụ truyenhaypro). Unique.

    [Required, MaxLength(200)] public string Name { get; set; } //Tên hiển thị tenant.

    [MaxLength(255)] public string Domain { get; set; } //tên miền tùy chỉnh

    [MaxLength(100)] public string SchemaName { get; set; } // Tên schema khi dùng schema-per-tenant (chuẩn hóa).

    [MaxLength(50)] public string Plan { get; set; } //Gói (free/pro/enterprise) — dùng để bật/tắt tính năng.

    public bool IsActive { get; set; } = true; //Có kích hoạt tenant hay không. Dùng để tắt nhanh tenant.

    public string ConfigJson { get; set; } // JSON cấu hình (feature flags, limits). Không chứa secrets.

    [MaxLength(200)] public string SecretsRef { get; set; } // tham chiếu đến KeyVault (KHÔNG lưu trữ bí mật)

    public ICollection<UserRole> UserRoles { get; set; }
    public AuthorProfile AuthorProfile { get; set; }
}