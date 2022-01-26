namespace EShop.Domain.Identity;

public class LoginResult
{
    public string JwtToken { get; set; }
    
    public string RefreshToken { get; set; }
}