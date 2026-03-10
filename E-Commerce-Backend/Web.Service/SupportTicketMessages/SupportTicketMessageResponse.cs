namespace Web.Service.SupportTicketMessages;
public record SupportTicketMessageResponse(
    int TicketId,
    string TicketTitle,
    int SenderId,
    string SenderName,
    string MessageContent,
    DateTime SentDate
    );
