using FluentValidation;

namespace Web.Service.Users.Create
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.UserLastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.UserEmail).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.UserEmail).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.UserPassword).MinimumLength(8).WithMessage("Password must be at least 8 characters");
            RuleFor(x => x.UserPhoneNumber).NotEmpty().WithMessage("Phone Number is required");
            RuleFor(x => x.UserPhoneNumber).Matches(@"^(\+[0-9]{10})$").WithMessage("Phone Number is not valid");
            RuleFor(x => x.UserDateOfBirth).NotEmpty().WithMessage("Date of Birth is required");
        }
    }
}