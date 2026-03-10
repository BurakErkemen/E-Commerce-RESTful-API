using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Repository.OrderSection.BasketItem;

namespace Web.Repository.OrderSection.Basket
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItemModel>
    {
        public void Configure(EntityTypeBuilder<BasketItemModel> builder)
        {
            // Primary key
            builder.HasKey(basketItem => basketItem.BasketItemId);

            // Relationship with Product (Many-to-One)
            builder.HasOne(basketItem => basketItem.Product)
                .WithMany()
                .HasForeignKey(basketItem => basketItem.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Do not delete products when BasketItem is deleted

            // Relationship with User (Many-to-One)
            builder.HasOne(basketItem => basketItem.User)
                .WithMany(user => user.BasketItems)
                .HasForeignKey(basketItem => basketItem.UserId)
                .OnDelete(DeleteBehavior.Cascade); // If the user is deleted, basket items are deleted

            // Quantity
            builder.Property(basketItem => basketItem.Quantity)
                .IsRequired();

            // Price
            builder.Property(basketItem => basketItem.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}