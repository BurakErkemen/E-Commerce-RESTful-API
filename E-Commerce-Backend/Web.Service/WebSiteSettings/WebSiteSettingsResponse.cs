namespace Web.Service.WebSiteSettings;
public record WebSiteSettingsResponse(
        string? SeoKeyword,
        string? Title,
        string? Description,
        string? Author,
        string? AuthorUrl,
        string Url,
        decimal FreeShippingThreshold,
        List<string> SupportedLanguages,
        string? FacebookUrl,
        string? TwitterUrl,
        string? InstagramUrl,
        string? LinkedInUrl,
        string? LogoUrl,
        string? FaviconUrl,
        List<int>? BannerIds // Banner'ların ID'lerini almak daha iyi olabilir.
    );

