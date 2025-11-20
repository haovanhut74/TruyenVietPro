using System.ComponentModel.DataAnnotations;
using TruyenHayPro.Infrastructure.Entities.Base;

namespace TruyenHayPro.Infrastructure.Entities.Product;

public class Category : TenantEntity
{
    [Required, MaxLength(200)] public string Name { get; set; }

    [MaxLength(100)] public string Slug { get; set; }

    public Guid? ParentCategoryId { get; set; } //phân cấp.
    public int OrderIndex { get; set; } = 0; //ordering.
}