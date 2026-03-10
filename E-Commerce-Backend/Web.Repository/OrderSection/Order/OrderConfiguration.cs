using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.OrderSection.Order;

public class OrderConfiguration : IEntityTypeConfiguration<OrderModel>
{
    public void Configure(EntityTypeBuilder<OrderModel> builder)
    {
        // Primary Key
        builder.HasKey(order => order.OrderId);

        // Relationship with User (One-to-Many)
        builder.HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade); // When user is deleted, orders are deleted

        // Order Status
        builder.Property(order => order.Status)
            .IsRequired()
            .HasConversion<string>(); // Enum as string

        // TotalAmount
        builder.Property(order => order.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Order Date
        builder.Property(order => order.OrderDate)
            .IsRequired();

        // Relationship with Payments
        builder.HasMany(order => order.Payments)
            .WithOne(payment => payment.Order)
            .HasForeignKey(payment => payment.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // When order is deleted, payments are deleted
    }
}
