using FluentValidation;

namespace Web.Service.Notifications.Update
{
    public class UpdateNotificationRequestValidator : AbstractValidator<UpdateNotificationRequest>
    {
        public UpdateNotificationRequestValidator() {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Notification not found!");

            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50)
                .WithMessage("E-Mail required!");

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Message content not empty!");
        }
    }
}
