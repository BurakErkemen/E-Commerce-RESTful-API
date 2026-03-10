using FluentValidation;

namespace Web.Service.BasketItem.Create
{
    public class CreateBasketItemRequestValidator : AbstractValidator<CreateBasketItemRequest>
    {
        public CreateBasketItemRequestValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("User is required.")
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.productId)
                .NotEmpty().WithMessage("Product is required.")
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
