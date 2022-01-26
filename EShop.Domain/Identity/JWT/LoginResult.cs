namespace EShop.Domain.Identity.JWT;

public class LoginResult
{
    public string JwtToken { get; set; }
    
    public string RefreshToken { get; set; }
}