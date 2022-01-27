using System.Collections.Generic;
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

    /// <summary>
    /// Send mail
    /// </summary>
    /// <param name="model">email model</param>
    /// <returns></returns>
    [HttpPost("mail")]
    public async Task<ActionResult> SendOneMailAsync([FromBody] EmailViewmodel model)
    {
        var result =
            await _smtpService.SendToOne(model.EmailTo, model.EmailToName, model.EmailSubject, model.EmailBody);

        if (!result)
        {
            return BadRequest("Smt went wrong!");
        }

        return Ok();
    }

    [HttpPost("mails")]
    public async Task<ActionResult> SendManyMailsAsync([FromBody] EmailsViewModel model)
    {
        await _smtpService.SendToAll(model.EmailsTo, model.EmailSubject, model.EmailBody);

        return Ok();
    }
}