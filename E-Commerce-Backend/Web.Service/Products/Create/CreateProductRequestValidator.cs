using FluentValidation;
using Web.Repository.ProductInfo.Products;

namespace Web.Service.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters.");

            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Product description is required.")
                .MinimumLength(10).WithMessage("Product description must be at least 10 characters.");

            RuleFor(x => x.ProductPrice)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.ProductStock)
                .GreaterThanOrEqualTo(0).WithMessage("Product stock must be zero or greater.");

            RuleFor(x => x.ProductStockCode)
                .NotEmpty().WithMessage("Product stock code is required.");

            RuleForEach(x => x.ProductImg)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Each image URL must be valid!");
        }
    }
}
