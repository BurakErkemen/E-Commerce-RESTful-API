using FluentValidation;

namespace Web.Service.Banners.Create
{
    public class CreateBannerValidator : AbstractValidator<CreateBannerRequest>
    {
        public CreateBannerValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("ImageUrl is required");

            RuleFor(x => x.BannerTitle).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("BannerTitle is required")
                .MaximumLength(100)
                .WithMessage("BannerTitle must not exceed 100 characters");

            RuleFor(x => x.BannerDisplayFrom)
                .LessThanOrEqualTo(x => x.BannerDisplayTo)
                .When(x => x.BannerDisplayFrom.HasValue && x.BannerDisplayTo.HasValue)
                .WithMessage("BannerDisplayFrom must be earlier than or equal to BannerDisplayTo.");

            RuleFor(x => x.IsActivate)
                .Must(x => x == true || x == false)
                .WithMessage("IsActivate must be true or false.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DisplayOrder must be zero or greater.");
        }
    }
}
