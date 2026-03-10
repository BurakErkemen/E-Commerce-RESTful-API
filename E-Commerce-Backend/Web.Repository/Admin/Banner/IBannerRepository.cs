namespace Web.Repository.Admin.Banner
{
    public interface IBannerRepository : IGenericRepository<BannerModel>
    {
        // Sadece aktif olan bannerları döndürür.
        Task<List<BannerModel>> GetBannersByActiveStatusAsync(bool isActive);

        // Sadece ilk aktif banner döndürülür.
        Task<BannerModel?> GetActiveBannerAsync();

        // Geçerli (aktif ve tarih aralığında olan) bannerları getir
        Task<List<BannerModel>> GetValidBannersAsync();

        Task<List<BannerModel>> GetBannersByIdsAsync(List<int>? bannerIds);
    }
}
