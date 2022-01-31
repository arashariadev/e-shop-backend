using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EShop.Domain.ThirdParty;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class NovaPoshtaController : ControllerBase
{
    private readonly INovaPoshtaHttpClient _poshtaHttpClient;

    public NovaPoshtaController(INovaPoshtaHttpClient poshtaHttpClient)
    {
        _poshtaHttpClient = poshtaHttpClient;
    }

    [HttpGet]
    public async Task<ActionResult<DeliveryStatusResponse>> GetDeliveryStatusAsync([FromQuery] string phone, [FromQuery, Required] string documentNumber)
    {
        var response = await _poshtaHttpClient.GetDeliveryStatusAsync(documentNumber, phone);

        return Ok(response);
    }
}