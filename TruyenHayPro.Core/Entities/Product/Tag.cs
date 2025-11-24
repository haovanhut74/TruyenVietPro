using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Core.Entities.Base;

namespace TruyenHayPro.Core.Entities;

public class Tag : TenantEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; } //tên tag hiển thị.

    [MaxLength(100)]
    public string Slug { get; set; } //slug tag.

    public int Count { get; set; } = 0; //số novels gán tag
}