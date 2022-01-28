using System.Net.Http;
using System.Threading.Tasks;
using EShop.Domain.Identity.Oauth2.Facebook;
using Newtonsoft.Json;

namespace EShop.Domain.Identity.Oauth2.Google;

public interface IGoogleService
{
    Task<GoogleUserInfo> GetUserInfoFromTokenAsync(string accessToken);
}

public class GoogleService : IGoogleService
{
    private const string Oauth2Url = "https://www.googleapis.com/oauth2/v3/userinfo?access_token={0}";
    private readonly IHttpClientFactory _httpClientFactory;

    public GoogleService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GoogleUserInfo> GetUserInfoFromTokenAsync(string accessToken)
    {
        var requestedUrl = string.Format(Oauth2Url, accessToken);

        var response = await _httpClientFactory.CreateClient().GetAsync(requestedUrl);
        response.EnsureSuccessStatusCode();

        var responseAsString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GoogleUserInfo>(responseAsString);

        return result;
    }
}