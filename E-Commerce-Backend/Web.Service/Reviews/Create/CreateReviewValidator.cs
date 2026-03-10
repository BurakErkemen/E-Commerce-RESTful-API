using FluentValidation;

namespace Web.Service.Reviews.Create
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product is required!");

            RuleFor(x => x.ReviewRating)
                .NotEmpty()
                .WithMessage("Rating is required!");
        }
    }
}