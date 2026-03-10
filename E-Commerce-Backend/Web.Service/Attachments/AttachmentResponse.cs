namespace Web.Service.Attachments;
public record AttachmentResponse(
    string FileName,
    string Url,
    long? FileSize,
    int TicketId, // or TicketTitle
    string TicketTitle,
    string UserName
    );