using System.Net;
using Web.Repository;
using Web.Repository.Admin.Banner;
using Web.Repository.Admin.WebSiteSettings;
using Web.Service.WebSiteSettings.Create;
using Web.Service.WebSiteSettings.Update;

namespace Web.Service.WebSiteSettings
{
    public class WebSiteSettingsService
        (IWebSiteSettingsRepository webRepository,
        IBannerRepository bannerRepository,
        IUnitOFWork unitOFWork) : IWebSiteSettingsService
    {
        public async Task<ServiceResult<WebSiteSettingsResponse?>> GetAsync(int id)
        {
            var settings = await webRepository.GetByIdAsync(id);

            if (settings == null) return ServiceResult<WebSiteSettingsResponse?>.Fail("Settings not found!", HttpStatusCode.NotFound);

            var response = new WebSiteSettingsResponse(
                settings.SeoKeyword,
                settings.Title,
                settings.Description,
                settings.Author,
                settings.AuthorUrl,
                settings.Url,
                settings.FreeShippingThreshold,
                settings.SupportedLanguages,
                settings.FacebookUrl,
                settings.TwitterUrl,
                settings.InstagramUrl,
                settings.LinkedInUrl,
                settings.LogoUrl,
                settings.FaviconUrl,
                settings.WebSiteBanners?.Select(b => b.BannerId).ToList()
            );
            return ServiceResult<WebSiteSettingsResponse?>.Success(response,HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult<CreateWebSiteSettingsResponse>> CreateAsync(CreateWebSiteSettingsRequest request)
        {
            var banners = await bannerRepository.GetBannersByIdsAsync(request.BannerIds);

            if (banners == null || banners.Count != request.BannerIds?.Count) 
                return ServiceResult<CreateWebSiteSettingsResponse>.Fail("Banner IDs not found!");
            
            // Yeni WebSiteSettingsModel nesnesi oluştur
            var newSettings = new WebSiteSettingsModel
            {
                SeoKeyword = request.SeoKeyword,
                Title = request.Title,
                Description = request.Description,
                Author = request.Author,
                AuthorUrl = request.AuthorUrl,
                Url = request.Url,
                FreeShippingThreshold = request.FreeShippingThreshold,
                SupportedLanguages = request.SupportedLanguages,
                FacebookUrl = request.FacebookUrl,
                TwitterUrl = request.TwitterUrl,
                InstagramUrl = request.InstagramUrl,
                LinkedInUrl = request.LinkedInUrl,
                LogoUrl = request.LogoUrl,
                FaviconUrl = request.FaviconUrl,
                WebSiteBanners = banners
            };

            await webRepository.AddAsync(newSettings);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateWebSiteSettingsResponse>.Success(new CreateWebSiteSettingsResponse(newSettings.Id));
        }

        public async Task<ServiceResult> UpdateFreeShippingAsync(UpdateFreeShippingAsync request)
        {
            var freeShippingSettings = await webRepository.GetByIdAsync(request.Id);

            if (freeShippingSettings == null) return ServiceResult.Fail("Settings not found!");

            freeShippingSettings.FreeShippingThreshold = request.FreeShippingThreshold;

            webRepository.Update(freeShippingSettings);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateLogoFavionFileAsync(UpdateLogoFavionFile request)
        {
            var logoFavionSettings = await webRepository.GetByIdAsync(request.Id);

            if (logoFavionSettings == null) return ServiceResult.Fail("Settings not found!");

            logoFavionSettings.LogoUrl = request.LogoURL;
            logoFavionSettings.FaviconUrl = request.FaviconURL;

            webRepository.Update(logoFavionSettings);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateSiteSettingsAsync(UpdateWebSiteSettingsRequest request)
        {
            var siteSettings = await webRepository.GetByIdAsync(request.Id);

            if (siteSettings == null) return ServiceResult.Fail("Settings not found!");

            siteSettings.SeoKeyword = request.Seo;
            siteSettings.Title = request.Title;
            siteSettings.Description = request.Description;
            siteSettings.Author = request.Author;
            siteSettings.AuthorUrl = request.AuthorURL;
            siteSettings.Url = request.URL!;

            webRepository.Update(siteSettings);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateSocialLinkAsync(UpdateSocialLinkRequest request)
        {
            var socialLinkSettings = await webRepository.GetByIdAsync(request.Id);

            if (socialLinkSettings == null) return ServiceResult.Fail("Settings not found!");

            socialLinkSettings.FacebookUrl = request.FacebookUrl;
            socialLinkSettings.TwitterUrl = request.TwitterUrl;
            socialLinkSettings.InstagramUrl = request.InstagramUrl;
            socialLinkSettings.LinkedInUrl = request.LinkedInUrl;

            webRepository.Update(socialLinkSettings);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateSupportedLanguages(UpdateSupportedLanguagesRequest request)
        {
            var supportedLanguages = await webRepository.GetByIdAsync(request.Id);

            if (supportedLanguages == null) return ServiceResult.Fail("Settings not found!");

            supportedLanguages.SupportedLanguages = request.SupportedLanguages!;

            webRepository.Update(supportedLanguages);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
