using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.ProductInfo.Reviews
{
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewModel>
    {
        public void Configure(EntityTypeBuilder<ReviewModel> builder)
        {
            // Primary Key
            builder.HasKey(r => r.ReviewsId);

            // Required fields and max lengths
            builder.Property(r => r.ReviewsComment)
                   .IsRequired()
                   .HasMaxLength(500); // İsteğe bağlı olarak karakter sınırlaması

            builder.Property(r => r.ReviewsRating)
                   .IsRequired()
                   .HasColumnType("smallint"); // Rating için smallint olarak belirleme

            builder.Property(r => r.ReviewsDateTime)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()"); // Varsayılan olarak geçerli tarih ve saat

            // User relationship
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Product relationship
            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}