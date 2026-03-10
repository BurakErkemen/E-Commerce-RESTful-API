namespace Web.Service.Banners.Create;
public record CreateBannerRequest(
    string ImageUrl,
    string BannerTitle,
    string? BannerLink, // Opsiyonel olabilir
    DateTime? BannerDisplayFrom, // Banner başlangıç tarihi (opsiyonel)
    DateTime? BannerDisplayTo,   // Banner bitiş tarihi (opsiyonel)
    bool IsActivate,               // Banner aktif durumu
    string? AdminNote,
    int DisplayOrder
    );