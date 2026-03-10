namespace Web.Repository.Admin.Banner
{
    public class BannerModel
    {
        public int BannerId { get; set; }
        public string? ImageUrl { get; set; }
        public string? BannerTitle { get; set; }
        public string? BannerLink { get; set; }

        // Banner'ın gösterim tarihi aralığı
        public DateTime? BannerDisplayFrom { get; set; }
        public DateTime? BannerDisplayTo { get; set; }

        // Banner'ın aktiflik durumu
        public bool IsActivate { get; set; } = true; // Varsayılan olarak aktif

        // Admin tarafından eklenen açıklama (örneğin, neden bu banner kullanılıyor)
        public string? AdminNote { get; set; }

        // Görünüm önceliği (daha düşük sayı öncelikli olur)
        public int DisplayOrder { get; set; } = 0;

        // Oluşturulma tarihi   
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
