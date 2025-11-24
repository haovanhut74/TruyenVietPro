using FluentValidation;
using TruyenHayPro.Application.Contracts.Category;
using TruyenHayPro.Application.Interfaces.Services;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories;

public static class CategoryAdminListEndpoint
{
    public static void MapCategoryAdminList(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (
                [AsParameters] CategoryListQuery query,
                IServiceCategory service,
                IValidator<CategoryListQuery> validator,
                CancellationToken ct) =>
            {
                // validate query
                var validation = await validator.ValidateAsync(query, ct);
                if (!validation.IsValid)
                {
                    // trả BadRequest kèm lỗi
                    var errors = validation.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage });
                    return Results.BadRequest(new { errors });
                }

                var result = await service.GetAdminListAsync(query, ct);
                return Results.Ok(result);
            })
            .WithName("CategoryAdmin_List");
    }
}