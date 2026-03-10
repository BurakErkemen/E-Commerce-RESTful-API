using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.OrderSection.Payment
{
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentModel>
    {
        public void Configure(EntityTypeBuilder<PaymentModel> builder)
        {
            // Primary key
            builder.HasKey(payment => payment.PaymentId);

            // Relationship with Order (Many-to-One)
            builder.HasOne(payment => payment.Order)
                .WithMany(order => order.Payments)
                .HasForeignKey(payment => payment.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // When order is deleted, payments are deleted

            // Payment Status
            builder.Property(payment => payment.Status)
                .IsRequired()
                .HasConversion<string>(); // Enum as string

            // Payment Method
            builder.Property(payment => payment.PaymentMethod)
                .IsRequired()
                .HasMaxLength(100);

            // TransactionId
            builder.Property(payment => payment.TransactionId)
                .IsRequired()
                .HasMaxLength(100);

            // Amount
            builder.Property(payment => payment.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Payment Date
            builder.Property(payment => payment.PaymentDate)
                .IsRequired();
        }
    }
}
