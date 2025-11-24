using FluentValidation;
using TruyenHayPro.Application.Contracts.Category;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories.Validators
{
    // Validator cho CategoryListQuery (dùng chung cho endpoint + service)
    public class CategoryListQueryValidator : AbstractValidator<CategoryListQuery>
    {
        public CategoryListQueryValidator()
        {
            // page phải >= 1
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page phải lớn hơn hoặc bằng 1");

            // pageSize trong khoảng 1..200
            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 200)
                .WithMessage("PageSize phải từ 1 đến 200");

            // Q: optional, tối đa 200 ký tự nếu có
            RuleFor(x => x.Q)
                .MaximumLength(200)
                .WithMessage("Từ khóa tìm kiếm quá dài (tối đa 200 ký tự)");
        }
    }
}