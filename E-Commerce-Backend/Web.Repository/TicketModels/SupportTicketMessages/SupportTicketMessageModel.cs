using Web.Repository.TicketModels.SupportTickets;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.TicketModels.SupportTicketMessages
{
    public class SupportTicketMessageModel
    {
        public int MessageId { get; set; }
        public int TicketId { get; set; } // İlgili talebin ID'si
        public int SenderId { get; set; } // Gönderen (Admin ya da Kullanıcı)
        public string MessageContent { get; set; } = default!; // Mesaj içeriği
        public DateTime SentDate { get; set; } // Mesaj gönderim tarihi


        // Navigation property
        public UserModel Sender { get; set; } = default!;
        public SupportTicketModel Ticket { get; set; } = default!;
    }
}
