namespace Web.Service.Banners.Update;
public record UpdateBannerRequest(
    int BannerId,
    string? ImageUrl,
    string? BannerTitle,
    string? BannerLink,
    DateTime? BannerDisplayFrom,
    DateTime? BannerDisplayTo,
    bool IsActivate,
    string? AdminNote,
    int DisplayOrder
    );