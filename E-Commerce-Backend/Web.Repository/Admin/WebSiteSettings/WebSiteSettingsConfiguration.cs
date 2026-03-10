using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Repository.Admin.WebSiteSettings
{
    public class WebSiteSettingsConfiguration : IEntityTypeConfiguration<WebSiteSettingsModel>
    {
        public void Configure(EntityTypeBuilder<WebSiteSettingsModel> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.SeoKeyword)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(w => w.Title)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(w => w.Description)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(w => w.Author)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(w => w.Url)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(w => w.FreeShippingThreshold)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Banner ile ilişki
            builder.HasMany(w => w.WebSiteBanners)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade); // Site silinirse banner'lar da silinsin
        }
    }
}
