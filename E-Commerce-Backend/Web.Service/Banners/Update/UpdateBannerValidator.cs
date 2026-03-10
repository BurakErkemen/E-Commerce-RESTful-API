using FluentValidation;

namespace Web.Service.Banners.Update
{
    public class UpdateBannerValidator : AbstractValidator<UpdateBannerRequest>
    {
        public UpdateBannerValidator()
        {
            // ImageUrl doğrulaması
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("ImageUrl cannot be empty when provided.");

            // BannerTitle doğrulaması,
            RuleFor(x => x.BannerTitle)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.BannerTitle))
                .WithMessage("BannerTitle must not exceed 100 characters.");

            // BannerDisplayFrom ve BannerDisplayTo her ikisi de varsa kontrol yapılır
            RuleFor(x => x.BannerDisplayFrom)
                .LessThanOrEqualTo(x => x.BannerDisplayTo)
                .When(x => x.BannerDisplayFrom.HasValue && x.BannerDisplayTo.HasValue)
                .WithMessage("BannerDisplayFrom must be earlier than or equal to BannerDisplayTo.");

            // DisplayOrder doğrulaması
            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DisplayOrder must be zero or greater.");

            // IsActivate doğrulaması
            RuleFor(x => x.IsActivate)
                .Must(x => x == true || x == false)
                .WithMessage("IsActivate must be true or false.");
        }
    }
}
