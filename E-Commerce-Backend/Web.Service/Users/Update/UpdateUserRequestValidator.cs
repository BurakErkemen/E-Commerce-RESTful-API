using FluentValidation;
using Web.Service.Categories.Create;

namespace Web.Service.Users.Update
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator() {

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.UserLastName)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.UserEmail)
                .NotEmpty().WithMessage("Email is required!")
                .EmailAddress().WithMessage("Invalid email format!");

            RuleFor(x => x.UserPassword)
                .NotEmpty().WithMessage("Password is required!")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long!");

            RuleFor(x => x.UserPhoneNumber)
                .NotEmpty().WithMessage("Phone number is required!")
                .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.")
                .MinimumLength(10).WithMessage("Phone number must be at least 10 digits.");

            RuleFor(x => x.UserDateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required!")
                .Must(BeAValidAge).WithMessage("User must be at least 18 years old!");

            RuleFor(x => x.MarketingConsent)
               .NotNull().WithMessage("Marketing consent is required.");
        }

        private bool BeAValidAge(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Now.AddYears(-18);
        }
    }
}
