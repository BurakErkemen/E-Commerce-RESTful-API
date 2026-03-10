using FluentValidation;

namespace Web.Service.UserAddress.Update
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        public UpdateAddressRequestValidator() {
            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("Address is required!");
            
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required!");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required!");
            
            RuleFor(x => x.PostCode)
                .NotEmpty().WithMessage("Post code is required!");
        }
    }
}
