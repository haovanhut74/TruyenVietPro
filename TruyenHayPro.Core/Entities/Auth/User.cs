using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;
using TruyenHayPro.Core.Entities.Role;

namespace TruyenHayPro.Core.Entities.Auth;

public class User : TenantEntity
{
    [Required, MaxLength(256)]
    public string Email { get; set; } //Email gốc; PII → phải mã hóa / Always Encrypted nếu cần.

    [Required, MaxLength(256)]
    public string EmailNormalized { get; set; } //Email chuẩn (lowercase) để search/unique index.

    [MaxLength(512)]
    public string PasswordHash { get; set; } // Hash mật khẩu (Argon2/bcrypt/PBKDF2). Không bao giờ lưu password thô.

    // Salt nếu dùng scheme lưu salt riêng.Với Argon2 có thể embed trong hash, nhưng vẫn để sẵn.
    [MaxLength(128)] public string PasswordSalt { get; set; }

    [MaxLength(100)] public string Username { get; set; } // Tên đăng nhập, unique per tenant nếu dùng.

    [MaxLength(200)] public string DisplayName { get; set; } //Tên hiển thị công khai

    public bool IsEmailConfirmed { get; set; } = false; //Email đã xác thực chưa.

    [MaxLength(20)] public string PhoneNumber { get; set; } // PII — encrypt-at-rest hoặc Always Encrypted

    [MaxLength(1000)] public string ProfilePictureUrl { get; set; } //CDN URL; nếu private, làm signed URL khi trả về.

    public bool IsActive { get; set; } = true; //user enable/disable.

    public DateTime? LastLoginAt { get; set; } //— tracking

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; }
    public AuthorProfile AuthorProfile { get; set; }
}