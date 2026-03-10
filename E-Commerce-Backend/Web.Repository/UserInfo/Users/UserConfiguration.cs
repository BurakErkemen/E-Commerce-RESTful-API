using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.UserInfo.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(25);
            builder.Property(u => u.UserLastName).IsRequired().HasMaxLength(20);
            builder.Property(u => u.UserEmail).IsRequired().HasMaxLength(50);
            builder.Property(u => u.UserPassword).IsRequired();

            builder.Property(u => u.UserRole)
                   .HasConversion<string>() // Enum'u string olarak kaydeder
                   .IsRequired();

            // Address ile bire-çok ilişki kurma
            builder.HasMany(u => u.Addresses)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}