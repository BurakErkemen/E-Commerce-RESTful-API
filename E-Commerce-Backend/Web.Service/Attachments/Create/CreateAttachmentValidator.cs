using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Repository;
using Web.Repository.TicketModels.SupportTickets;

namespace Web.Service.Attachments.Create;

public class CreateAttachmentValidator : AbstractValidator<CreateAttachmentRequest>
{
    public CreateAttachmentValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("Attachment name is required.")
            .MinimumLength(3).WithMessage("Attachment name must be at least 3 characters.");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Attachment URL is required.")
            .Must(url => !string.IsNullOrWhiteSpace(url)).WithMessage("Attachment URL cannot be whitespace.")
            .MinimumLength(3).WithMessage("Attachment URL must be at least 3 characters.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("Attachment file size must be greater than 0.");

        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("Ticket ID is required.");
    }
}
