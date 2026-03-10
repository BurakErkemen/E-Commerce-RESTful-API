using Web.Repository.TicketModels.Attachments;
using Web.Repository.TicketModels.SupportTicketMessages;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.TicketModels.SupportTickets
{
    public enum SupportTicketStatus
    {
        Open,
        InProgress,
        Closed
    }
    public enum SupportTicketPriority
    {
        Low,
        Medium,
        High
    }
    public class SupportTicketModel
    {
        public int TicketId { get; set; }
        public int UserId { get; set; } // Talebi açan kullanıcı
        public string Title { get; set; } = default!; // Talebin kısa başlığı
        public string Description { get; set; } = default!; // Talebin detaylı açıklaması
        public SupportTicketStatus Status { get; set; } = SupportTicketStatus.Open; // Açık, Çözümde, Kapalı
        public SupportTicketPriority Priority { get; set; } = SupportTicketPriority.Medium; // Düşük, Orta, Yüksek
        public DateTime CreatedDate { get; set; } // Talep oluşturma tarihi
        public DateTime UpdateDate { get; set; } // Talep oluşturma tarihi
        public DateTime? ClosedDate { get; set; } // Talebin kapanış tarihi


        // Navigation property
        public UserModel User { get; set; } = default!;
        public virtual ICollection<AttachmentModel> Attachments { get; set; } = new List<AttachmentModel>();
        public ICollection<SupportTicketMessageModel> Messages { get; set; } = new List<SupportTicketMessageModel>();
    }


}