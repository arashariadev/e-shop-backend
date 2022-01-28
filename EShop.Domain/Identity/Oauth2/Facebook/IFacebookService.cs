using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EShop.Domain.Identity.Oauth2.Facebook;

public interface IFacebookService
{
    Task<FacebookValidationResult> ValidateAccessTokenAsync(string accessToken);

    Task<FacebookUserInfo> GetUserInfoByToken(string accessToken);
}

public class FacebookService : IFacebookService
{
    private const string TokenValidationUrl = "";
    private const string UserInfoUrl = "";
    private readonly FacebookAuthSettings _authSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public FacebookService(FacebookAuthSettings authSettings, IHttpClientFactory httpClientFactory)
    {
        _authSettings = authSettings;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<FacebookValidationResult> ValidateAccessTokenAsync(string accessToken)
    {
        var requestedUrl = string.Format(TokenValidationUrl, accessToken, _authSettings.AppId, _authSettings.AppSecret);

        var response = await _httpClientFactory.CreateClient().GetAsync(requestedUrl);
        response.EnsureSuccessStatusCode();

        var responseAsString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<FacebookValidationResult>(responseAsString);

        return result;
    }

    public async Task<FacebookUserInfo> GetUserInfoByToken(string accessToken)
    {
        var requestedUrl = string.Format(UserInfoUrl, accessToken);

        var response = await _httpClientFactory.CreateClient().GetAsync(requestedUrl);
        response.EnsureSuccessStatusCode();

        var responseAsString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<FacebookUserInfo>(responseAsString);

        return result;
    }
}