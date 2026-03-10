using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.TicketModels.SupportTickets
{
    public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicketModel>
    {
        public void Configure(EntityTypeBuilder<SupportTicketModel> builder)
        {
            // Primary key
            builder.HasKey(ticket => ticket.TicketId);

            // User ile ilişki (One-to-Many)
            builder.HasOne(ticket => ticket.User)
                .WithMany(user => user.SupportTickets)
                .HasForeignKey(ticket => ticket.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Silme davranışı

            // Title
            builder.Property(ticket => ticket.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Description
            builder.Property(ticket => ticket.Description)
                .IsRequired()
                .HasMaxLength(2000);

            // Status enum olarak saklanıyor
            builder.Property(ticket => ticket.Status)
                .IsRequired()
                .HasConversion<string>(); // Enum'u string olarak sakla

            // Priority enum olarak saklanıyor
            builder.Property(ticket => ticket.Priority)
                .IsRequired()
                .HasConversion<string>(); // Enum'u string olarak sakla

            // Attachments
            builder.HasMany(ticket => ticket.Attachments)
                   .WithOne(attachment => attachment.Ticket)
                   .HasForeignKey(attachment => attachment.TicketId) // Açık foreign key tanımı
                   .OnDelete(DeleteBehavior.Cascade); // Silme davranışı


            // CreatedDate
            builder.Property(ticket => ticket.CreatedDate)
                .IsRequired();

            // ClosedDate
            builder.Property(ticket => ticket.ClosedDate)
                .IsRequired(false); // Opsiyonel alan
        }
    }
}
