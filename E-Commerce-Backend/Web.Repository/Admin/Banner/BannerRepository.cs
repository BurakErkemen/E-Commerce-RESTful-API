using Microsoft.EntityFrameworkCore;

namespace Web.Repository.Admin.Banner
{
    public class BannerRepository(WebDbContext context) :
        GenericRepository<BannerModel>(context), IBannerRepository
    {
        public async Task<List<BannerModel>> GetBannersByActiveStatusAsync(bool isActive)
        {
            if (isActive == false)
            {
                return await Context.Banners
                    .Where(b => b.IsActivate == false)
                    .ToListAsync();
            }

            return await Context.Banners
                .Where(b => b.IsActivate)
                .ToListAsync();
        }

        // İlk aktif olan bannerı döndür
        public async Task<BannerModel?> GetActiveBannerAsync()
        {
            return await Context.Banners
                .Where(b => b.IsActivate)
                .OrderByDescending(b => b.BannerDisplayFrom) // İstenirse sıralama eklenebilir
                .FirstOrDefaultAsync();
        }

        // Geçerli bannerları getir
        public async Task<List<BannerModel>> GetValidBannersAsync()
        {
            return await Context.Banners
                .Where(b => b.IsActivate &&
                (!b.BannerDisplayFrom.HasValue || b.BannerDisplayFrom <= DateTime.Now) &&
                (!b.BannerDisplayTo.HasValue || b.BannerDisplayTo >= DateTime.Now))
                .ToListAsync();
        }

        public async Task<List<BannerModel>> GetBannersByIdsAsync(List<int>? bannerIds)
        {
            if (bannerIds == null || !bannerIds.Any())
                return new List<BannerModel>();

            return await Context.Banners
                .Where(b => bannerIds.Contains(b.BannerId))
                .ToListAsync();
        }
    }
}