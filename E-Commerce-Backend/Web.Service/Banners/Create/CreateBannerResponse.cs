namespace Web.Service.Banners.Create;

// Yeni oluşturulan bannerın benzersiz kimliği
public record CreateBannerResponse(int BannerId);

/*
  string ImageUrl,       // Bannerın resmi için URL
    string BannerTitle,    // Bannerın başlığı
    string? BannerLink,    // Bannerın bağlantısı (opsiyonel)
    DateTime? DisplayFrom, // Gösterimin başlangıç tarihi (opsiyonel)
    DateTime? DisplayTo,   // Gösterimin bitiş tarihi (opsiyonel)
    bool IsActive,         // Bannerın aktif durumu
    string? AdminNote,
    int DisplayOrder
*/