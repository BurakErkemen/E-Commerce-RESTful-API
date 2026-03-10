using Web.Repository.TicketModels.SupportTickets;

namespace Web.Service.SupportTickets.Create;
public record CreateSupportTicketRequest(
    int UserId,
    string Title,
    string Description,
    SupportTicketStatus Status = SupportTicketStatus.Open,
    SupportTicketPriority Priority = SupportTicketPriority.Low
);