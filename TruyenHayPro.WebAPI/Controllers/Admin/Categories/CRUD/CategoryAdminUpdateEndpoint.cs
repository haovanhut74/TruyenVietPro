using FluentValidation;
using TruyenHayPro.Application.Contracts.Category;
using TruyenHayPro.Application.Interfaces.Services;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories.CRUD;

public static class CategoryAdminUpdateEndpoint
{
    public static void MapCategoryAdminUpdate(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}", async (
            Guid id,
            CategoryUpdateRequest req,
            IServiceCategory service,
            IValidator<CategoryUpdateRequest> validator,
            CancellationToken ct) =>
        {
            var validation = await validator.ValidateAsync(req, ct);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage });
                return Results.BadRequest(new { errors });
            }

            try
            {
                var ok = await service.UpdateAsync(id, req, ct);
                return ok ? Results.NoContent() : Results.NotFound();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}