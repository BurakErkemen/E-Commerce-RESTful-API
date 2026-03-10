using FluentValidation;

namespace Web.Service.UserAddress.Create
{
    public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        public CreateAddressRequestValidator()
        {
            RuleFor(x => x.AddressLine).NotEmpty().WithMessage("Address Line is required");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("State is required");
            RuleFor(x => x.PostCode).NotEmpty().WithMessage("Post Code is required");
            RuleFor(x => x.PostCode).Matches(@"^(\d{5})$").WithMessage("Post Code is not valid");
        }

    }
}
