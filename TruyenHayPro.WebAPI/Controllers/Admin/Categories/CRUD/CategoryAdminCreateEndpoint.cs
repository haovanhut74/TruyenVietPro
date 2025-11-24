using FluentValidation;
using TruyenHayPro.Application.Contracts.Category;
using TruyenHayPro.Application.Interfaces.Services;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories.CRUD;

public static class CategoryAdminCreateEndpoint
{
    public static void MapCategoryAdminCreate(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            CategoryCreateRequest req,
            IServiceCategory service,
            IValidator<CategoryCreateRequest> validator,
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
                var id = await service.CreateAsync(req, ct);
                return Results.Created($"/api/admin/categories/{id}", null);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}