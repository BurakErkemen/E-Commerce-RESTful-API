using FluentValidation;

namespace Web.Service.Categories.Update
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required!")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long!");

            RuleFor(x => x.ParentCategoryId)
                .GreaterThan(0).When(x => x.ParentCategoryId.HasValue)
                .WithMessage("Parent category ID must be a positive number!");
        }
    }
}
