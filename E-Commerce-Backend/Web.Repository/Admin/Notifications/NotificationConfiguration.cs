using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.Admin.Notifications
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationModel>
    {
        public void Configure(EntityTypeBuilder<NotificationModel> builder)
        {
            builder.HasKey(x => x.NotificationId);
            builder.Property(x => x.UserEmail).IsRequired();
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.MessageDate).IsRequired();

            builder.HasOne(x => x.Users)
                .WithMany()
                .HasForeignKey(x => x.UserEmail)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
