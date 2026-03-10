using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.Service.AuthServer;
using Web.Service.Users;

namespace Web.API.Controllers
{
    [Route("api/auth")] 
    [ApiController]
    public class AuthServerController(ITokenHandler tokenHandler,IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenRequestModel request)
        {
            // Kullanıcıyı kimlik doğrulama
            var user = await userService.GetUserFindAsync(request.Email, request.Password);

            // Kullanıcı doğrulama sonucu
            if (user == null || user.Data == null)
                return Unauthorized("Invalid credentials");

            // Erişim ve yenileme token'ları oluşturuluyor
            var accessToken = await tokenHandler.CreateAccessToken(30, user.Data.UserId, user.Data.UserRole.ToString());
            var refreshToken = await tokenHandler.CreateRefreshToken(1440, user.Data.UserId); // Refresh token 24 saat geçerli

            return Ok(new
            {
                AccessToken = accessToken?.AccessToken,
                Expiration = accessToken?.Expiration,
                RefreshToken = refreshToken?.RefreshToken,
                RefreshTokenExpiration = refreshToken?.Expiration
            });
        }

        // Refresh Token Endpoint
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
        {
            // Refresh token'ın geçerliliğini kontrol et
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Refresh token is required.");
            }

            // Token'ı doğrulama
            var stringToken = request.RefreshToken;

            // Refresh token'ı kullanıcı ile eşleştir
            var refreshToken = await tokenHandler.GetRefreshTokenByTokenAsync(stringToken);
            if (refreshToken == null || refreshToken.UserId != request.UserID)
            {
                return Unauthorized("Invalid refresh token");
            }

            // Yeni erişim ve yenileme token'ları oluşturuluyor
            var newAccessToken = await tokenHandler.CreateAccessToken(30, refreshToken.UserId, refreshToken.Role);
            var newRefreshToken = await tokenHandler.CreateRefreshToken(1440, refreshToken.UserId);

            return Ok(new
            {
                AccessToken = newAccessToken?.AccessToken,
                Expiration = newAccessToken?.Expiration,
                RefreshToken = newRefreshToken?.RefreshToken,
                RefreshTokenExpiration = newRefreshToken?.Expiration
            });
        }

    }
}
