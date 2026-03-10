namespace Web.Service.SupportTicketMessages.Update;
public record UpdateSupportTicketMessageRequest(
    int MessageId,
    string MessageContent
    );