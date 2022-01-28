using Newtonsoft.Json;

namespace EShop.Domain.Identity.Oauth2.Facebook;

public class FacebookUserInfo
{
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty("last_name")]
    public string LastName { get; set; }
    
    [JsonProperty("picture")]
    public FacebookPicture Picture { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("data")]
    public FacebookData Data { get; set; }
}

public class FacebookPicture
{
    [JsonProperty("data")]
    public FacebookData Data { get; set; }
}

public class FacebookData
{
    [JsonProperty("height")]
    public long Height { get; set; }
    
    [JsonProperty("is_silhouette")]
    public bool IsSilhouette { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("width")]
    public string Width { get; set; }
}