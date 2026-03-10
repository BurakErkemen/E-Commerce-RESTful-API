using FluentValidation;

namespace Web.Service.SupportTicketMessages.Update
{
    public class UpdateSupportTicketMessageValidator : AbstractValidator<UpdateSupportTicketMessageRequest>
    {
        public UpdateSupportTicketMessageValidator()
        {
            // MessageId'nin sıfırdan büyük olması gerektiğini doğrula
            RuleFor(x => x.MessageId)
                .GreaterThan(0)
                .WithMessage("MessageId must be greater than zero.");

            // MessageContent'in boş olmaması ve makul bir uzunlukta olması gerektiğini doğrula
            RuleFor(x => x.MessageContent)
                .NotEmpty()
                .WithMessage("MessageContent cannot be empty.")
                .MaximumLength(500)
                .WithMessage("MessageContent cannot exceed 500 characters.");
        }
    }
}
