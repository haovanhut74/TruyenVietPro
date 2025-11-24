using TruyenHayPro.Core.Entities.Auth;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities.Service;

public class Comment : TenantEntity
{
    public Guid? NovelId { get; set; } //nếu comment trên truyện.
    public Novel Novel { get; set; }

    public Guid? ChapterId { get; set; } //nếu comment trên chương.
    public Chapter Chapter { get; set; }

    public Guid? UserId { get; set; } //tác giả comment; null nếu anonymous.
    public User User { get; set; }

    public string Content { get; set; } // nội dung; sanitize & moderation.

    //đã được phê duyệt bởi người quản lý hoặc quy trình làm việc được phê duyệt tự động.
    public bool IsApproved { get; set; } = false;
    public bool IsFlagged { get; set; } = false; //tự động gắn cờ

    public Guid? ParentCommentId { get; set; } //chủ đề trả lời.
    public Comment ParentComment { get; set; }

    public int LikeCount { get; set; } = 0; //like của comment.
}