using Web.Repository.TicketModels.SupportTickets;

namespace Web.Service.SupportTickets.Update;
public record UpdateSupportTicketRequest(
    string Title,
    string Description,
    SupportTicketStatus Status = SupportTicketStatus.Open,
    SupportTicketPriority Priority = SupportTicketPriority.Low,
    DateTime? ClosedDate = null
    );