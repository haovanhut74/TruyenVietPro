using TruyenHayPro.Application.Interfaces.Services;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories.CRUD;

public static class CategoryAdminDeleteEndpoint
{
    public static void MapCategoryAdminDelete(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (Guid id, IServiceCategory service, CancellationToken ct) =>
        {
            try
            {
                var ok = await service.DeleteAsync(id, ct);
                return ok ? Results.NoContent() : Results.NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}