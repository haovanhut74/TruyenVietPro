using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

public class SeoMetadata : AuditedEntity
{
    [MaxLength(50)] public string EntityType { get; set; } // Novel/Chapter
    public Guid EntityId { get; set; }
    public string CanonicalUrl { get; set; } //URL chuẩn.
    [MaxLength(300)] public string MetaTitle { get; set; } //SEO title.
    [MaxLength(500)] public string MetaDescription { get; set; } //mô tả meta.
    public string OgImageUrl { get; set; } //hình ảnh đồ thị mở.
    public string StructuredJsonLd { get; set; } //JSON-LD.
}