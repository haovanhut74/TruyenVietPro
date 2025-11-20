using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Manager;

// 16. MediaAsset
public class MediaAsset : TenantEntity
{
    public Guid? OwnerId { get; set; } // chủ sở hữu tùy chọn (ID người dùng hoặc ID tiểu thuyết)
    [MaxLength(50)] public string Type { get; set; } // hình ảnh/âm thanh/tệp đính kèm
    [MaxLength(2000)] public string Url { get; set; } // CDN
    [MaxLength(1000)] public string StorageKey { get; set; } // khóa đối tượng ở S3
    [MaxLength(100)] public string MimeType { get; set; } //image/jpeg, audio/mpeg
    public long? SizeBytes { get; set; } //kích thước
    public bool IsPrivate { get; set; } = false; //nếu true, phải dùng signed URL khi truy xuất
    public Guid? UploadedBy { get; set; }
}