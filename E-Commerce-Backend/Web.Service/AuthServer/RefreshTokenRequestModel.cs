namespace Web.Service.AuthServer;
public class RefreshTokenRequestModel
{
    public int UserID { get; set; }
    public string? RefreshToken { get; set; } 
}
