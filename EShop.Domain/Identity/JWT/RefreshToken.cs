using System;

namespace EShop.Domain.Identity.JWT;

public class RefreshToken
{
    public string Token { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset ExpirationTime { get; set; }

    public string Id { get; set; }

    public bool IsExpired => ExpirationTime <= DateTimeOffset.Now;
}