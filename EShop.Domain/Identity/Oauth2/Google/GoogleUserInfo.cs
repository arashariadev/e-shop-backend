using Newtonsoft.Json;

namespace EShop.Domain.Identity.Oauth2.Google;

public class GoogleUserInfo
{
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("email_verified")]
    public bool IsVerified { get; set; }
    
    [JsonProperty("name")]
    public string FullName { get; set; }
    
    [JsonProperty("picture")]
    public string PictureUri { get; set; }
    
    [JsonProperty("given_Name")]
    public string FirstName { get; set; }
    
    [JsonProperty("family_name")]
    public string LastName { get; set; }
    
    [JsonProperty("locale")]
    public string Locale { get; set; }
}