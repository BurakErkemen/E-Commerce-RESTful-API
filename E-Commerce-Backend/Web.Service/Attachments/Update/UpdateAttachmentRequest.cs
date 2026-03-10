namespace Web.Service.Attachments.Update;
public record UpdateAttachmentRequest(
    int Id,
    string FileName,
    string Url,
    long FileSize
    );