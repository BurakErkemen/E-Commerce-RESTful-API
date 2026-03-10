using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Repository;
using Web.Repository.UserInfo.Token;
using Web.Repository.UserInfo.Users;

namespace Web.Service.AuthServer
{
    public class TokenHandler
        (IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IUnitOFWork unitOFWork,
        IConfiguration configuration) : ITokenHandler
    {
        // Erişim token'ı oluşturma
        public async Task<TokenResponseModel?> CreateAccessToken(int min, int userId, string userRole)
        {
            TokenResponseModel tokenResponseModel = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var user = await userRepository.GetByIdAsync(userId);

            if (user is null)
            {
                return null; // Kullanıcı bulunamazsa null döndür
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, userRole)
            };

            tokenResponseModel.Expiration = DateTime.UtcNow.AddMinutes(min);
            JwtSecurityToken securityToken = new(
                audience: configuration["Token:Audience"],
                issuer: configuration["Token:Issuer"],
                expires: tokenResponseModel.Expiration,
                notBefore: DateTime.UtcNow,
                claims: claims, // Claim'leri buraya dahil ettik
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            tokenResponseModel.AccessToken = tokenHandler.WriteToken(securityToken);
            return tokenResponseModel;
        }

        // Yenileme token'ı oluşturma
        public async Task<RefreshTokenResponseModel?> CreateRefreshToken(int min, int userId)
        {
            var refreshToken = Guid.NewGuid().ToString(); // Rastgele bir refresh token oluşturma
            var expiration = DateTime.UtcNow.AddMinutes(min);

            var newRefreshToken = new RefreshTokenModel
            {
                UserId = userId,
                Token = refreshToken,
                ExpiryDate = expiration,
                IsRevoked = false,
                IsUsed = false
            };

            await refreshTokenRepository.AddAsync(newRefreshToken); // Yeni refresh token'ı ekle
            await unitOFWork.SaveChangesAsync(); // Değişiklikleri kaydet

            return new RefreshTokenResponseModel
            {
                RefreshToken = refreshToken,
                Expiration = expiration
            };
        }
        // Refresh token'ı token'a göre al
        public async Task<RefreshTokenModel?> GetRefreshTokenByTokenAsync(string token)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(token);

            // Refresh token'ın doğruluğunu kontrol et
            if (refreshToken != null && !refreshToken.IsUsed && !refreshToken.IsRevoked)
            {
                return refreshToken;
            }

            return null; // Geçersiz veya kullanılmış/iptal edilmiş token
        }
    }
}
