using Microsoft.EntityFrameworkCore;
using System.Net;
using Web.Repository;
using Web.Repository.Admin.Banner;
using Web.Service.Banners.Create;
using Web.Service.Banners.Update;

namespace Web.Service.Banners
{
    public class BannerService
        (IBannerRepository bannerRepository,
        IUnitOFWork unitOFWork) : IBannerService
    {
        public async Task<ServiceResult<BannerResponse>> GetBannerByIdAsync(int bannerId)
        {
            var banner = await bannerRepository.GetByIdAsync(bannerId);

            if (banner is null)
                return ServiceResult<BannerResponse>.Fail("Banner not found!", HttpStatusCode.NotFound);

            #region Mapping
            var bannerAsResponse = new BannerResponse(
                banner.ImageUrl,
                banner.BannerTitle,
                banner.BannerLink,
                banner.BannerDisplayTo,
                banner.BannerDisplayFrom,
                banner.IsActivate,
                banner.AdminNote,
                banner.DisplayOrder,
                banner.CreatedAt,
                banner.UpdatedAt
                );
            #endregion

            return ServiceResult<BannerResponse>.Success(bannerAsResponse);
        }

        public async Task<ServiceResult<List<BannerResponse>>> GetAllBannersAsync()
        {
            var banners = await bannerRepository.GetAll().ToListAsync();

            // BannerResponse'e dönüştür
            var bannerAsResponse = banners.Select(b => new BannerResponse(
                b.ImageUrl,
                b.BannerTitle,
                b.BannerLink,
                b.BannerDisplayFrom,
                b.BannerDisplayTo,
                b.IsActivate,
                b.AdminNote,
                b.DisplayOrder,
                b.CreatedAt,
                b.UpdatedAt
            )).ToList();

            return ServiceResult<List<BannerResponse>>.Success(bannerAsResponse);
        }

        public async Task<ServiceResult<CreateBannerResponse>> CreateBannerAsync(CreateBannerRequest request)
        {
            var banner = new BannerModel
            {
                ImageUrl = request.ImageUrl,
                BannerTitle = request.BannerTitle,
                BannerLink = request.BannerLink,
                BannerDisplayFrom = request.BannerDisplayFrom,
                BannerDisplayTo = request.BannerDisplayTo,
                IsActivate = request.IsActivate,
                AdminNote = request.AdminNote,
                DisplayOrder = request.DisplayOrder
            };

            await bannerRepository.AddAsync(banner);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateBannerResponse>.Success(new CreateBannerResponse(banner.BannerId));
        }

        public async Task<ServiceResult> UpdateBannerAsync(UpdateBannerRequest request)
        {
            var banner = await bannerRepository.GetByIdAsync(request.BannerId);
            if (banner is null)
                return ServiceResult.Fail("Banner not found!", HttpStatusCode.NotFound);

            banner.ImageUrl = request.ImageUrl;
            banner.BannerTitle = request.BannerTitle;
            banner.BannerLink = request.BannerLink;
            banner.BannerDisplayFrom = request.BannerDisplayFrom;
            banner.BannerDisplayTo = request.BannerDisplayTo;
            banner.IsActivate = request.IsActivate;
            banner.AdminNote = request.AdminNote;
            banner.DisplayOrder = request.DisplayOrder;
            banner.UpdatedAt = DateTime.Now;
            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteBannerAsync(int bannerId)
        {
            var banner = await bannerRepository.GetByIdAsync(bannerId);

            if (banner is null)
                return ServiceResult.Fail("Banner not found", HttpStatusCode.NotFound);

            bannerRepository.Delete(banner);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }



        public async Task<ServiceResult<BannerResponse?>> GetActiveBannerAsync()
        {
            var activeBanner = await bannerRepository.GetActiveBannerAsync();

            if (activeBanner is null)
                return ServiceResult<BannerResponse?>.Fail("Active banner not found!", HttpStatusCode.NotFound);

            var bannerResponse = new BannerResponse(
                activeBanner.ImageUrl,
                activeBanner.BannerTitle,
                activeBanner.BannerLink,
                activeBanner.BannerDisplayFrom,
                activeBanner.BannerDisplayTo,
                activeBanner.IsActivate,
                activeBanner.AdminNote,
                activeBanner.DisplayOrder,
                activeBanner.CreatedAt,
                activeBanner.UpdatedAt
                );

            return ServiceResult<BannerResponse?>.Success(bannerResponse);
        }

        public async Task<ServiceResult<List<BannerResponse>>> GetBannersByActiveStatusAsync(bool ısActivate)
        {
            var banner = await bannerRepository.GetBannersByActiveStatusAsync(ısActivate);

            if (banner is null)
                return ServiceResult<List<BannerResponse>>.Fail("Active banners not found!", HttpStatusCode.NotFound);

            var bannerResponse = banner.Select(b => new BannerResponse(
                b.ImageUrl,
                b.BannerTitle,
                b.BannerLink,
                b.BannerDisplayFrom,
                b.BannerDisplayTo,
                b.IsActivate,
                b.AdminNote,
                b.DisplayOrder,
                b.CreatedAt,
                b.UpdatedAt
                )).ToList();

            return ServiceResult<List<BannerResponse>>.Success(bannerResponse);
        }

        public async Task<ServiceResult<List<BannerResponse>>> GetValidBannersAsync()
        {
            var banner = await bannerRepository.GetValidBannersAsync();

            if (banner == null || !banner.Any())
                return ServiceResult<List<BannerResponse>>.Fail("Active banners not found!", HttpStatusCode.NotFound);

            var bannerResponse = banner.Select(b => new BannerResponse(
                b.ImageUrl,
                b.BannerTitle,
                b.BannerLink,
                b.BannerDisplayFrom,
                b.BannerDisplayTo,
                b.IsActivate,
                b.AdminNote,
                b.DisplayOrder,
                b.CreatedAt,
                b.UpdatedAt
                )).ToList();

            return ServiceResult<List<BannerResponse>>.Success(bannerResponse);
        }
    }
}
