using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.ProductInfo.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(p => p.ProductID);
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.ProductDescription).IsRequired().HasMaxLength(250);
            builder.Property(p => p.ProductPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.ProductStock).IsRequired();
            builder.Property(p => p.ProductImg).IsRequired();
            builder.Property(p => p.ProductStockCode).IsRequired();

            builder.HasOne(p => p.Category) // Kategori ilişkisi
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.CategoryId)
           .OnDelete(DeleteBehavior.Cascade); // Kategori silinirse ürünler de silinir

            // OrderItem İlişkisi
            builder.HasMany(p => p.BasketItems)
                   .WithOne(oi => oi.Product)
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
