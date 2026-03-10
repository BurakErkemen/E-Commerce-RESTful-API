using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Service.Banners;
using Web.Service.Banners.Create;
using Web.Service.Banners.Update;

namespace Web.API.Controllers
{
    [Route("api/banner/")]
    [ApiController]
    public class BannerController(IBannerService bannerService) : CustomBaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateBanner(CreateBannerRequest request)
        {
            var bannerResult = await bannerService.CreateBannerAsync(request);

            return CreateActionResult(bannerResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBanner(UpdateBannerRequest request)
        {
            var bannerResult = await bannerService.UpdateBannerAsync(request);

            return CreateActionResult(bannerResult);
        }
        [HttpDelete("delete/{bannerId:int}")]
        public async Task<IActionResult> DeleteBanner(int bannerId)
        {
            var bannerResult = await bannerService.DeleteBannerAsync(bannerId);

            return CreateActionResult(bannerResult);
        }
        [HttpGet("getbyId/{bannerId:int}")]
        public async Task<IActionResult> GetBannerById(int bannerId)
        {
            var bannerResult = await bannerService.GetBannerByIdAsync(bannerId);

            return CreateActionResult(bannerResult);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllBanners()
        {
            var bannerResult = await bannerService.GetAllBannersAsync();

            return CreateActionResult(bannerResult);
        }
        [HttpGet("getbyActiveStatus/{isActivate:bool}")]
        public async Task<IActionResult> GetBannersByActiveStatus(bool isActivate)
        {
            var bannerResult = await bannerService.GetBannersByActiveStatusAsync(isActivate);

            return CreateActionResult(bannerResult);
        }
        [HttpGet("getActiveBanner")]
        public async Task<IActionResult> GetActiveBanner()
        {
            var bannerResult = await bannerService.GetActiveBannerAsync();

            return CreateActionResult(bannerResult);
        }
        [HttpGet("getValidBanners")]
        public async Task<IActionResult> GetValidBanners()
        {
            var bannerResult = await bannerService.GetValidBannersAsync();

            return CreateActionResult(bannerResult);
        }
    }
}
