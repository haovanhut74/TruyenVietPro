namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories;

public static class CategoryAdminEndpointGroup
{
    public static RouteGroupBuilder MapCategoryAdminGroup(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/admin/categories")
            .RequireAuthorization("AdminPolicy"); // Policy: Admin

        return group;
    }
}