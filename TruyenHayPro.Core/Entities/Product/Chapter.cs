using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

public class Chapter : TenantEntity
{
    [Required] public Guid NovelId { get; set; } //FK đến Novel.
    public Novel Novel { get; set; }

    public int Number { get; set; } // thứ tự chương (1,2,...). Dùng để sort; unique per novel.

    [MaxLength(300)]
    public string Slug { get; set; } // slug SEO tùy chọn (duy nhất cho mỗi tiểu thuyết nếu được áp dụng).

    [Required, MaxLength(300)] public string Title { get; set; } //tiêu đề chương

    // nội dung chính; sanitize trước render; có thể lưu raw ở nơi khác cho moderation.
    public string Content { get; set; }

    [MaxLength(1000)] public string ContentUrl { get; set; } // nếu lưu content trên S3/CDN.

    public int WordCount { get; set; } // đếm từ để tính giá, thống kê.

    public bool IsPaid { get; set; } = false; //chương trả phí không.

    public decimal? Price { get; set; } // giá chương.

    public bool IsVisible { get; set; } = true; //ẩn/hiện.

    public long Views { get; set; } = 0; //lượt xem chương.

    public DateTime? PublishedAt { get; set; } //ngày publish chương.

    [MaxLength(128)] public string Checksum { get; set; } // hash nội dung (integrity / change detection).
}