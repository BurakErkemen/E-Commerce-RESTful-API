using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Repository.TicketModels.SupportTickets;

namespace Web.Repository.TicketModels.Attachments
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentModel>
    {
        public void Configure(EntityTypeBuilder<AttachmentModel> builder)
        {
            // Primary key
            builder.HasKey(a => a.Id);

            // FileName property
            builder.Property(a => a.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

            // Url property
            builder.Property(a => a.Url)
                   .IsRequired()
                   .HasMaxLength(500);

            // FileSize property
            builder.Property(a => a.FileSize)
                   .IsRequired(false) // Opsiyonel alan
                   .HasColumnType("bigint"); // Veritabanında bigint türü

            // Foreign key relationship
            builder.HasOne(a => a.Ticket)
                   .WithMany(t => t.Attachments)
                   .HasForeignKey(a => a.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
