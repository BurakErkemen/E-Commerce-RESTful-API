namespace Web.Service.AuthServer;
public class RefreshTokenResponseModel
{
    public string RefreshToken { get; set; } = default!;
    public DateTime Expiration { get; set; }
}
