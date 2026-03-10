using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Service;
using Web.Service.WebSiteSettings;
using Web.Service.WebSiteSettings.Create;
using Web.Service.WebSiteSettings.Update;

namespace Web.API.Controllers
{
    [Route("api/websitesettings/")]
    [ApiController]
    public class WebSiteSettingsController(IWebSiteSettingsService webSiteSettingsService) : CustomBaseController
    {
        [HttpGet("getId/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await webSiteSettingsService.GetAsync(id);

            return CreateActionResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateWebSiteSettingsRequest request)
        {
            var result = await webSiteSettingsService.CreateAsync(request);

            return CreateActionResult(result);
        }
        
        [HttpPatch("updateSocialLink")]
        public async Task<IActionResult> UpdateSocialLink(UpdateSocialLinkRequest request)
        {
            var result = await webSiteSettingsService.UpdateSocialLinkAsync(request);

            return CreateActionResult(result);
        }
        
        [HttpPatch("updateFreeShipping")]
        public async Task<IActionResult> UpdateFreeShipping(UpdateFreeShippingAsync request)
        {
            var result = await webSiteSettingsService.UpdateFreeShippingAsync(request);

            return CreateActionResult(result);
        }
        
        [HttpPatch("updateLogoFavionFile")]
        public async Task<IActionResult> UpdateLogoFavionFile(UpdateLogoFavionFile request)
        {
            var result = await webSiteSettingsService.UpdateLogoFavionFileAsync(request);

            return CreateActionResult(result);
        }
        
        [HttpPatch("updateSiteSettings")]
        public async Task<IActionResult> UpdateSiteSettings(UpdateWebSiteSettingsRequest request)
        {
            var result = await webSiteSettingsService.UpdateSiteSettingsAsync(request);

            return CreateActionResult(result);
        }

        [HttpPatch("updateSupportedLanguages")]
        public async Task<IActionResult> UpdateSupportedLanguages(UpdateSupportedLanguagesRequest request)
        {
            var result = await webSiteSettingsService.UpdateSupportedLanguages(request);

            return CreateActionResult(result);
        }
    }
}
