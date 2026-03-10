using FluentValidation;

namespace Web.Service.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator() {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required!")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long!");

            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Product description is required!");

            RuleFor(x => x.ProductPrice)
                .NotEmpty().WithMessage("Price is required!")
                .GreaterThan(0).WithMessage("Price must be a positive value!");

            RuleFor(x => x.ProductStock)
                .GreaterThanOrEqualTo(0).WithMessage("Product stock cannot be negative!");

            RuleFor(x => x.ProductStockCode)
                .NotEmpty().WithMessage("Stock code is required!");

            RuleForEach(x => x.ProductImg)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Each image URL must be valid!");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("A valid category ID is required!");
        }
    }
}
