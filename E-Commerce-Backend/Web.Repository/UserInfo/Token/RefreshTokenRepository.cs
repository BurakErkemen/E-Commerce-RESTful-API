using Microsoft.EntityFrameworkCore;

namespace Web.Repository.UserInfo.Token
{
    public class RefreshTokenRepository(WebDbContext context) : GenericRepository<RefreshTokenModel>(context), IRefreshTokenRepository
    {

        // Token'a göre refresh token'ı almak için metod
        public async Task<RefreshTokenModel?> GetByTokenAsync(string token)
        {
            // Token'a göre refresh token'ı getirmek için sorgu
            return await Context.RefreshTokens
                .Where(r => r.Token == token && !r.IsUsed && !r.IsRevoked)  // İptal edilmiş veya kullanılan token'ları dışarıda bırak
                .FirstOrDefaultAsync();
        }
    }
}