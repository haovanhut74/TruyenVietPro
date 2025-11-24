namespace TruyenHayPro.Application.Contracts.Category;

public class CategoryListQuery
{
    // Trang hiện tại
    public int Page { get; set; } = 1;

    // Số item/trang
    public int PageSize { get; set; } = 20;

    // Từ khoá tìm kiếm (tên)
    public string? Q { get; set; }
}