using TruyenHayPro.Application.DTO.Admin;
using TruyenHayPro.Application.DTO.User;
using TruyenHayPro.Core.Entities;

namespace TruyenHayPro.Application.Interfaces.Repository;

/// Repository dành cho Category.
/// Tách biệt rõ ràng giữa public và admin, tối ưu cho hệ thống nhiều tenant.
/// Không lộ dữ liệu nhạy cảm, không trả entity ngoài mục đích cần thiết.
public interface IRepoCategory
{
    /// Lấy entity Category theo Id.
    /// Trả về null nếu không tồn tại hoặc bị soft-delete bởi global filter.
    Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default);

    // Trả IQueryable<CategoryDto> cho giao diện Public.
    /// Chỉ gồm dữ liệu nhẹ, không bao gồm TenantId, audit hay NovelCount.
    /// Caller có thể áp dụng filter/paging/sort trước khi materialize.
    IQueryable<CategoryDto> QueryAsPublic();

    // Trả IQueryable<CategoryAdminDto> cho giao diện Admin.
    /// Gồm đầy đủ thông tin: TenantId, ParentName, NovelCount, audit...
    /// Không load navigation, chỉ dùng projection an toàn và hiệu năng cao.
    IQueryable<CategoryAdminDto> QueryAsAdmin();

    /// Thêm Category mới.
    /// Không commit ngay; caller chịu trách nhiệm gọi SaveChangesAsync.
    /// Đảm bảo tính bảo mật: chỉ thêm vào tenant hiện tại (context filter).
    Task AddAsync(Category entity, CancellationToken ct = default);

    /// Đánh dấu entity sẽ được cập nhật.
    /// Chỉ sử dụng khi entity đã attach vào DbContext.
    void Update(Category entity);

    /// Soft-delete Category bằng cách đặt IsDeleted = true.
    /// Không xoá vật lý để giữ tính toàn vẹn dữ liệu (audit + history).
    void SoftDelete(Category entity);

    /// Kiểm tra tồn tại Category theo Id.
    /// Tự động áp dụng global filter (soft-delete + tenant).
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);

    /// Kiểm tra Slug có bị trùng hay không.
    /// Dùng để validate input trước khi tạo/cập nhật.
    Task<bool> ExistsBySlugAsync(string slug, CancellationToken ct = default);

    /// Lưu thay đổi vào database.
    /// Caller chủ động xác định thời điểm commit để đảm bảo tính nhất quán.
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}