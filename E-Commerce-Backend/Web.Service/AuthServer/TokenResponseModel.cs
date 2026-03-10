namespace Web.Service.AuthServer;
public class TokenResponseModel
{
    public string? AccessToken { get; set; }
    public DateTime Expiration { get; set; }

    public string? RefreshToken { get; set; } 
    public DateTime RefreshTokenExpiration { get; set; } 
}

