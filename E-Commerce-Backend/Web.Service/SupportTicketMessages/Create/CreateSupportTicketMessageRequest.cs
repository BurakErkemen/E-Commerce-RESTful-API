namespace Web.Service.SupportTicketMessages.Create;
public record CreateSupportTicketMessageRequest(
    int TicketId,
    int SenderId,
    string MessageContent
    );