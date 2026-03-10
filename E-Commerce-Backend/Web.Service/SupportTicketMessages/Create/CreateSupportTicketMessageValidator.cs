using FluentValidation;

namespace Web.Service.SupportTicketMessages.Create;
public class CreateSupportTicketMessageValidator: AbstractValidator<CreateSupportTicketMessageRequest>
{
    public CreateSupportTicketMessageValidator()
    {
         RuleFor(x => x.TicketId)
                .GreaterThan(0)
                .WithMessage("TicketId must be greater than zero.");

            RuleFor(x => x.SenderId)
                .GreaterThan(0)
                .WithMessage("SenderId must be greater than zero.");

            RuleFor(x => x.MessageContent)
                .NotEmpty()
                .WithMessage("MessageContent cannot be empty.")
                .MaximumLength(500)
                .WithMessage("MessageContent cannot exceed 500 characters.");        
    }
}