using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EShop.Domain.ThirdParty.CurrencyApi;

public interface IPrivatBankHttpClient
{
    Task<IEnumerable<CurrentExchangeRatesResponse>> GetCurrentExchangeRatesAsync();
}

public class PrivatBankHttpClient : IPrivatBankHttpClient
{
    private const string ExchangeRateUrl = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
    private readonly IHttpClientFactory _httpClientFactory;

    public PrivatBankHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<CurrentExchangeRatesResponse>> GetCurrentExchangeRatesAsync()
    {
        var requestedUrl = string.Format(ExchangeRateUrl);

        var response = await _httpClientFactory.CreateClient().GetAsync(requestedUrl);
        response.EnsureSuccessStatusCode();

        var responseAsString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<IEnumerable<CurrentExchangeRatesResponse>>(responseAsString);

        return result;
    }
}