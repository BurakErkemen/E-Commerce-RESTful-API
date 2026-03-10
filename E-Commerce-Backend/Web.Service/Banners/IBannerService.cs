using Web.Service.Banners.Create;
using Web.Service.Banners.Update;

namespace Web.Service.Banners
{
    public interface IBannerService
    {
        Task<ServiceResult<CreateBannerResponse>> CreateBannerAsync(CreateBannerRequest request);
        Task<ServiceResult> UpdateBannerAsync(UpdateBannerRequest request);
        Task<ServiceResult> DeleteBannerAsync(int bannerId);
        Task<ServiceResult<BannerResponse>> GetBannerByIdAsync(int bannerId);
        Task<ServiceResult<List<BannerResponse>>> GetAllBannersAsync();

        Task<ServiceResult<List<BannerResponse>>> GetBannersByActiveStatusAsync(bool ısActivate);
        Task<ServiceResult<BannerResponse?>> GetActiveBannerAsync();
        Task<ServiceResult<List<BannerResponse>>> GetValidBannersAsync();

    }
}
