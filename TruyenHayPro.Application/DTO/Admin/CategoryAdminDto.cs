namespace TruyenHayPro.Application.DTO.Admin;

public record CategoryAdminDto(
    Guid Id,
    Guid TenantId, // admin cần biết tenant context
    string Name,
    string? Slug,
    Guid? ParentCategoryId,
    string? ParentCategoryName, // nếu có thể projection lấy được
    int OrderIndex,
    bool IsDeleted,
    DateTime CreatedAt,
    Guid? CreatedById,
    DateTime? UpdatedAt,
    Guid? UpdatedById,
    int NovelCount // số truyện liên kết (correlated subquery / cached)
);