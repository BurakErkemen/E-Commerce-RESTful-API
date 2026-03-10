namespace Web.Service.WebSiteSettings.Update;
public record UpdateWebSiteSettingsRequest(
    int Id,
    string? Seo,
    string? Title,
    string? Description,
    string? Author,
    string? AuthorURL,
    string? URL
    );

public record UpdateSocialLinkRequest(
    int Id,
    string? FacebookUrl,
    string? TwitterUrl,
    string? InstagramUrl,
    string? LinkedInUrl
    );

public record UpdateFreeShippingAsync(
    int Id,
    decimal FreeShippingThreshold
    );

public record UpdateLogoFavionFile(
    int Id,
    string? LogoURL,
    string? FaviconURL
    );

public record UpdateSupportedLanguagesRequest(
    int Id,
    List<string>? SupportedLanguages
    );