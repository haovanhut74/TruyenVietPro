using FluentValidation;
using TruyenHayPro.Application.Contracts.Category;

namespace TruyenHayPro.WebAPI.Controllers.Admin.Categories.Validators
{
    // Validator cho Create request
    public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidator()
        {
            // Name bắt buộc, tối đa 200 ký tự
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(200).WithMessage("Tên tối đa 200 ký tự");

            // Slug optional nhưng nếu có thì valid length + pattern (basic)
            RuleFor(x => x.Slug)
                .MaximumLength(100).WithMessage("Slug tối đa 100 ký tự")
                .Matches(@"^[a-z0-9\-]*$").When(x => !string.IsNullOrEmpty(x.Slug))
                .WithMessage("Slug chỉ được chứa chữ thường, số và dấu -");

            // OrderIndex >= 0
            RuleFor(x => x.OrderIndex)
                .GreaterThanOrEqualTo(0).WithMessage("OrderIndex phải >= 0");
        }
    }
}