using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Repository.TicketModels.SupportTickets;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.TicketModels.SupportTicketMessages
{
    public class SupportTicketMessageConfiguration : IEntityTypeConfiguration<SupportTicketMessageModel>
    {
        public void Configure(EntityTypeBuilder<SupportTicketMessageModel> builder)
        {
            builder.HasKey(msg => msg.MessageId); // Mesajın birincil anahtarı

            // Foreign Key (İlişkili ticket ve sender bilgileri)
            builder.HasOne(msg => msg.Sender) // Gönderen kullanıcıyla ilişki
                   .WithMany() // Bir kullanıcı birden fazla mesaj gönderebilir
                   .HasForeignKey(msg => msg.SenderId)
                   .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silindiğinde mesaj silinmesin

            builder.HasOne(msg => msg.Ticket) // Talep ile ilişki
                   .WithMany(ticket => ticket.Messages) // Bir talep birden fazla mesaj alabilir
                   .HasForeignKey(msg => msg.TicketId)
                   .OnDelete(DeleteBehavior.Cascade); // Talep silindiğinde mesajlar da silinsin

            // MessageContent uzunluğunu sınırlayalım
            builder.Property(msg => msg.MessageContent)
                   .IsRequired() // Mesaj içeriği zorunlu
                   .HasMaxLength(2000); // Mesaj uzunluğunu sınırlayalım

            // SentDate'ı zorunlu hale getirelim
            builder.Property(msg => msg.SentDate)
                    .IsRequired()
                    .HasColumnType("datetime2"); // Hassas tarih formatı


        }
    }
}
