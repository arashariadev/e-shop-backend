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
    /// Send mail to one user
    /// </summary>
    /// <param name="model">email model</param>
    /// <response code="200">Email success sent</response>
    /// <response code="400">Smtp is die or incorrect login and password for smtp</response>
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

    /// <summary>
    /// Send mail to multiple users
    /// </summary>
    /// <param name="model">Emails model</param>
    /// <response code="200">Emails success sent</response>
    [HttpPost("mails")]
    public async Task<ActionResult> SendManyMailsAsync([FromBody] EmailsViewModel model)
    {
        await _smtpService.SendToAll(model.EmailsTo, model.EmailSubject, model.EmailBody);

        return Ok();
    }
}