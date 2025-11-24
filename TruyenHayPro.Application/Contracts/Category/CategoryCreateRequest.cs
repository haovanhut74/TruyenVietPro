namespace TruyenHayPro.Application.Contracts.Category;

public class CategoryCreateRequest
{
    // Tên category
    public string Name { get; set; } = string.Empty;

    // Slug tự nhập hoặc để repo tự generate
    public string? Slug { get; set; }

    // Category cha (nếu có)
    public Guid? ParentCategoryId { get; set; }

    // Thứ tự hiển thị
    public int OrderIndex { get; set; } = 0;
}