using TruyenHayPro.Application.Common;
using TruyenHayPro.Application.Contracts.Category;
using TruyenHayPro.Application.DTO.Admin;

namespace TruyenHayPro.Application.Interfaces.Services
{
    // Giao diện service xử lý nghiệp vụ cho Category
    // - Không truy cập DB trực tiếp
    // - Chỉ sử dụng Repo
    // - Dùng để phục vụ API/Admin
    public interface IServiceCategory
    {
        // Lấy danh sách Category cho Admin (phân trang)

        Task<PagedResult<CategoryAdminDto>> GetAdminListAsync(
            CategoryListQuery query, 
            CancellationToken ct = default);


        // Lấy 1 Category chi tiết (dùng cho trang edit)
        Task<CategoryAdminDto?> GetAdminDetailAsync(
            Guid id,
            CancellationToken ct = default);

        // Tạo mới Category
        Task<Guid> CreateAsync(
            CategoryCreateRequest request,
            CancellationToken ct = default);

        // Cập nhật Category
        Task<bool> UpdateAsync(
            Guid id,
            CategoryUpdateRequest request,
            CancellationToken ct = default);

        // Xóa Category (soft delete)
        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken ct = default);
    }
}