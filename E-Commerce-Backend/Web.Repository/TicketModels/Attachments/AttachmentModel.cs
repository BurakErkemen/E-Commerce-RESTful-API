using Web.Repository.TicketModels.SupportTickets;

namespace Web.Repository.TicketModels.Attachments
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = default!;
        public string Url { get; set; } = default!;
        public long? FileSize { get; set; } // Dosya boyutu (opsiyonel)


        // Foreign Key
        public int TicketId { get; set; } // İlgili ticket ID

        // Navigation property
        public virtual SupportTicketModel Ticket { get; set; } = default!;
    }
}