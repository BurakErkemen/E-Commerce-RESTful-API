namespace Web.Service.Attachments.Create;
public record CreateAttachmentRequest(
    string FileName,
    string Url,
    long FileSize,
    int TicketId
    );