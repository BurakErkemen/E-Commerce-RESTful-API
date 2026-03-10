using Web.Repository.TicketModels.SupportTickets;

namespace Web.Service.SupportTickets.Update;
public record UpdateTicketStatusRequest(
    int TicketId,
    SupportTicketStatus Status
    );