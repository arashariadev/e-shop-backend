using System.Threading.Tasks;
using EShop.Api.Models.Smtp;
using EShop.Domain.Smtp;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SmtpController : ControllerBase
{
    private readonly ISmtpService _smtpService;

    public SmtpController(ISmtpService smtpService)
    {
        _smtpService = smtpService;
    }

    [HttpPost]
    public async Task<ActionResult> SendOneMailAsync([FromBody] EmailDataViewmodel model)
    {
        var result =
            await _smtpService.SentToOne(model.EmailTo, model.EmailToName, model.EmailSubject, model.EmailBody);

        if (!result)
        {
            return BadRequest("smt went wrong");
        }

        return Ok();
    }
}