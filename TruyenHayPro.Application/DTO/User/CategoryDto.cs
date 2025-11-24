namespace TruyenHayPro.Application.DTO.User;

public record CategoryDto(
    Guid Id,
    string Name,
    string? Slug,
    Guid? ParentCategoryId,
    int OrderIndex
);