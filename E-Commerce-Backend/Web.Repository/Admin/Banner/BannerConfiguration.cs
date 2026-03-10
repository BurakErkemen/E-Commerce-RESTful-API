using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Web.Repository.Admin.Banner
{
    public class BannerConfiguration : IEntityTypeConfiguration<BannerModel>
    {
        public void Configure(EntityTypeBuilder<BannerModel> builder)
        {
            builder.HasKey(b => b.BannerId);

            builder.Property(b => b.ImageUrl)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(b => b.BannerTitle)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(b => b.BannerLink)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(b => b.BannerDisplayFrom)
                   .IsRequired(false);

            builder.Property(b => b.BannerDisplayTo)
                   .IsRequired(false);
        }
    }
}
