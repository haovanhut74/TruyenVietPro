using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Auth;

public class AuthorProfile : TenantEntity
{
    [Required] public Guid UserId { get; set; } //FK → User; duy nhất (1:1).
    public User User { get; set; }

    [MaxLength(200)] public string DisplayName { get; set; } //tên công khai.

    public string Bio { get; set; } // giới thiệu; sanitize.

    [MaxLength(500)] public string Website { get; set; } //link tác giả.

    public string SocialLinksJson { get; set; } //danh sách social JSON.

    [MaxLength(1000)] public string AvatarUrl { get; set; } //ảnh đại diện.

    public int FollowersCount { get; set; } = 0; //số follower (update async).

    public bool IsVerified { get; set; } = false; //tick xanh.
}