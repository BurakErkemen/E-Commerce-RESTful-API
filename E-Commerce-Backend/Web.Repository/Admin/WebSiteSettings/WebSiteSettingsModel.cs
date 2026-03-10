using System.Reflection;
using Web.Repository.Admin.Banner;

namespace Web.Repository.Admin.WebSiteSettings
{
    public class WebSiteSettingsModel
    {
        public int Id { get; set; }
        public string? SeoKeyword { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? Author { get; set; } = null; // Web sitesinin sahibi veya yazarıyla ilgili bilgi sağlıyor.
        public string? AuthorUrl { get; set; } = null;
        public string Url { get; set; } = default!;

        // Ücretsiz kargo alt limiti
        public decimal FreeShippingThreshold { get; set; }

        // Sosyal medya bağlantıları
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedInUrl { get; set; }

        // Logo ve favicon dosyaları
        public string? LogoUrl { get; set; }
        public string? FaviconUrl { get; set; }

        // Dil desteği
        public List<string> SupportedLanguages { get; set; } = new List<string> { "en", "tr" };


        // Banner listesi
        public List<BannerModel>? WebSiteBanners { get; set; }
    }
}