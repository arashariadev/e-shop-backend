using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using static System.Net.Mime.MediaTypeNames;

namespace EShop.Domain.ThirdParty;

public interface INovaPoshtaHttpClient
{
    Task<DeliveryStatusResponse> GetDeliveryStatusAsync(string documentNumber, string phone);
}

public class NovaPoshtaHttpClient : INovaPoshtaHttpClient
{
    private const string TrackingUrl = "https://api.novaposhta.ua/v2.0/json";
    private readonly IHttpClientFactory _httpClient;
    private readonly NovaPoshtaSettings _novaPoshtaSettings;

    public NovaPoshtaHttpClient(IHttpClientFactory httpClient, NovaPoshtaSettings novaPoshtaSettings)
    {
        _httpClient = httpClient;
        _novaPoshtaSettings = novaPoshtaSettings;
    }

    public async Task<DeliveryStatusResponse> GetDeliveryStatusAsync(string documentNumber, string phone)
    {
        //TODO data is invalid suka
        var requestModel = new DeliveryStatusRequest
        {
            ApiKey = _novaPoshtaSettings.Key,
            ModelName = "TrackingDocument",
            CalledMethod = "getStatusDocument",
            MethodProperties = new MethodProperties()
            {
                Documents = new []
                {
                    new Document()
                    {
                        DocumentNumber = documentNumber,
                        Phone = phone
                    }
                }
            }
        };

        var json = JsonConvert.SerializeObject(requestModel);

        var requestModelJson = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.CreateClient().PostAsync(TrackingUrl, requestModelJson);

        var responseAsString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeliveryStatusResponse>(responseAsString);

        return result;
    }
}