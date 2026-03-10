using FluentValidation;

namespace Web.Service.Notifications.Create
{
    public class CreateNotificationRequestValidator : AbstractValidator<CreateNotificationRequest>
    {
        public CreateNotificationRequestValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message content is required!");
        }
    }
}
