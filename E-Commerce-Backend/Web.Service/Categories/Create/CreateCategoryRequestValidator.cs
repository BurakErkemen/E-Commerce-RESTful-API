using FluentValidation;

namespace Web.Service.Categories.Create
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required!")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long!")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters!");
        }
    }
}