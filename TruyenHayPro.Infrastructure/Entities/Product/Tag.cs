using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities;

public class Tag : TenantEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; } //tên tag hiển thị.

    [MaxLength(100)]
    public string Slug { get; set; } //slug tag.

    public int Count { get; set; } = 0; //số novels gán tag
}