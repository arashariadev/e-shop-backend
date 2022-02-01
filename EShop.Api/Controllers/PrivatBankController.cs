using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Domain.ThirdParty.CurrencyApi;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PrivatBankController : ControllerBase
{
    private readonly IPrivatBankHttpClient _privatBankHttpClient;

    public PrivatBankController(IPrivatBankHttpClient privatBankHttpClient)
    {
        _privatBankHttpClient = privatBankHttpClient;
    }

    [HttpGet("exchange-rate")]
    public async Task<ActionResult<IEnumerable<CurrentExchangeRatesResponse>>> GetCurrentExchangeRatesAsync()
    {
        var currentCurrency = await _privatBankHttpClient.GetCurrentExchangeRatesAsync();

        return Ok(currentCurrency);
    }
}