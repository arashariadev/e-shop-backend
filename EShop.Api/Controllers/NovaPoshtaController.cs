using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EShop.Api.Models.NovaPoshta;
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

    [HttpPost]
    public async Task<ActionResult<DeliveryStatusResponse>> GetDeliveryStatusAsync([FromBody] GetDeliveryStatusModel model)
    {
        var response = await _poshtaHttpClient.GetDeliveryStatusAsync(model.DocumentNumber, model.Phone);

        return Ok(response);
    }
}