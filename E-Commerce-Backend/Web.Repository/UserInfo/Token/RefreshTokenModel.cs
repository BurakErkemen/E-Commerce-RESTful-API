namespace Web.Repository.UserInfo.Token;
public class RefreshTokenModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }
    public string Role { get; set; } = "User";

}
