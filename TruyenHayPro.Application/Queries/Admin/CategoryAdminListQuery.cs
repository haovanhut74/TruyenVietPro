namespace TruyenHayPro.Application.Queries.Admin;

public record CategoryAdminListQuery(
    int Page = 1,
    int PageSize = 20,
    string? Search = null
);