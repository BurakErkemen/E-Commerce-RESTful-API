using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.UserInfo.UserAddresses
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddressModel>
    {
        public void Configure(EntityTypeBuilder<UserAddressModel> builder)
        {
            builder.HasKey(u => u.AddressId);

            builder.Property(u => u.AddressLine).IsRequired().HasMaxLength(128);
            builder.Property(u => u.City).IsRequired().HasMaxLength(25);
            builder.Property(u => u.Country).IsRequired().HasMaxLength(25);
            builder.Property(u => u.PostCode).IsRequired().HasMaxLength(10);
            builder.Property(u => u.AddressNote).HasMaxLength(128);

            builder.HasOne(u => u.User)
                   .WithMany()
                   .HasForeignKey(u => u.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // İndeks eklemek için
            builder.HasIndex(u => u.City);
            builder.HasIndex(u => u.Country);
            builder.HasIndex(u => u.PostCode);

        }
    }
}
