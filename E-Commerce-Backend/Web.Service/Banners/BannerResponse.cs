namespace Web.Service.Banners;
public record BannerResponse(
    string? ImageUrl,
    string? BannerTitle,
    string? BannerLink,
    DateTime? BannerDisplayFrom,
    DateTime? BannerDisplayTo,
    bool IsActivate,
    string? AdminNote,
    int DisplayOrder,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );