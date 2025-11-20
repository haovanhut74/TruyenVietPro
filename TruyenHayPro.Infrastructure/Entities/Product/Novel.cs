using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class Novel : TenantEntity
{
    [Required]
    public Guid AuthorId { get; set; } // user id của tác giả (FK). Nếu có AuthorProfile thì phần mapping tương ứng.

    [Required, MaxLength(200)] public string Slug { get; set; } //SEO slug. Unique per tenant.

    [Required, MaxLength(300)] public string Title { get; set; } //tiêu đề.

    public string Summary { get; set; } // mô tả ngắn; sanitize HTML.

    [Required, MaxLength(50)] public string Status { get; set; } // Bản nháp/Đã xuất bản/Đã lưu trữ.

    [MaxLength(10)] public string Language { get; set; } = "vi"; //mã ngôn ngữ (vi/en).

    [MaxLength(1000)] public string CoverUrl { get; set; } // CDN URL. ảnh 

    [MaxLength(500)] public string Tags { get; set; } //(nếu lưu CSV); ưu tiên M:N NovelTag

    public bool IsPaid { get; set; } = false; //có paywall hay không.

    [MaxLength(50)] public string PriceModel { get; set; } //per-chapter/subscription.

    [MaxLength(300)] public string SeoTitle { get; set; } //override SEO.

    [MaxLength(500)] public string SeoDescription { get; set; } //SEO meta.

    public string SeodataJson { get; set; } //JSON-LD có cấu trúc.

    public long Views { get; set; } = 0; //xem, cập nhật gia tăng (sử dụng bộ nhớ đệm).

    public int Likes { get; set; } = 0; //số like.

    public decimal? RatingAvg { get; set; } //trung bình (cập nhật bằng công việc)

    public DateTime? PublishedAt { get; set; } //ngày publish.

    public ICollection<Chapter> Chapters { get; set; }
    public ICollection<NovelTag> NovelTags { get; set; }
    public ICollection<NovelCategory> NovelCategories { get; set; }
}