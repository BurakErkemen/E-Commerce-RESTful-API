using Web.Service.WebSiteSettings.Create;
using Web.Service.WebSiteSettings.Update;

namespace Web.Service.WebSiteSettings
{
    public interface IWebSiteSettingsService
    {
        Task<ServiceResult<WebSiteSettingsResponse?>> GetAsync(int id);

        Task<ServiceResult<CreateWebSiteSettingsResponse>> CreateAsync(CreateWebSiteSettingsRequest request);
        Task<ServiceResult> UpdateSocialLinkAsync(UpdateSocialLinkRequest request);
        Task<ServiceResult> UpdateFreeShippingAsync(UpdateFreeShippingAsync request);
        Task<ServiceResult> UpdateLogoFavionFileAsync(UpdateLogoFavionFile request);
        Task<ServiceResult> UpdateSiteSettingsAsync(UpdateWebSiteSettingsRequest request);
        Task<ServiceResult> UpdateSupportedLanguages(UpdateSupportedLanguagesRequest request);
    }
}
