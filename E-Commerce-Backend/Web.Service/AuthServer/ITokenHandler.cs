using Web.Repository.UserInfo.Token;

namespace Web.Service.AuthServer
{
    public interface ITokenHandler
    {
        Task<TokenResponseModel?> CreateAccessToken(int min, int id, string userRole);
        Task<RefreshTokenResponseModel?> CreateRefreshToken(int min, int id);
        Task<RefreshTokenModel?> GetRefreshTokenByTokenAsync(string token);
    }
}
