using TruyenHayPro.Application.Interfaces.Services;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories.CRUD;

public static class CategoryAdminDetailEndpoint
{
    public static void MapCategoryAdminDetail(this RouteGroupBuilder group)
    {
        // GET api/admin/categories/{id}
        group.MapGet("/{id:guid}", async (
                Guid id,
                IServiceCategory service,
                CancellationToken ct) =>
            {
                // gọi service lấy dữ liệu
                var dto = await service.GetAdminDetailAsync(id, ct);

                // nếu không tìm thấy
                if (dto == null)
                    return Results.NotFound(new { message = "Không tìm thấy danh mục." });

                // trả kết quả
                return Results.Ok(dto);
            })
            .WithName("CategoryAdmin_Detail");
    }
}