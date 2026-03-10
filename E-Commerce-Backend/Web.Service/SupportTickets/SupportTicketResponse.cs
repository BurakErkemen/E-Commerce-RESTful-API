using Web.Repository.TicketModels.SupportTickets;
namespace Web.Service.SupportTickets;
public record SupportTicketResponse
(
    int TicketId, 
    string UserFullName, // Kullanıcı tam adı
    string Title,
    string Description,
    SupportTicketStatus Status,
    SupportTicketPriority Priority,
    DateTime CreatedDate ,
     DateTime? ClosedDate
    //public List<AttachmentResponse>? Attachments { get; set; }
    //public List<SupportTicketMessageResponse>? Messages { get; set; }
);