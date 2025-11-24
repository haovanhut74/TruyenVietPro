namespace TruyenHayPro.Application.Contracts.Category;

public class CategoryUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public int OrderIndex { get; set; }
}