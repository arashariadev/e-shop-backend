using System.Threading.Tasks;
using EShop.Api.Helpers;
using EShop.Api.Models;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model">Registration model</param>
        /// <response code="201">User account successfully created</response>
        /// <response code="400">Validation failed</response>
        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegistrationAsync([FromBody] RegistrationUserViewModel model)
        {
            var result = await _identityService.RegistrationAsync(model.FirstName, model.LastName, model.PhoneNumber,
                model.Email, model.Password, model.ConfirmPassword);

            if (!result.Successed)
            {
                return BadRequest(result.ToProblemDetails());
            }

            return Ok();
        }
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">Login model</param>
        /// <response code="201">Successful login</response>
        /// <response code="400">Wrong email/password combination</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            var (result, token) = await _identityService.LoginAsync(model.Email, model.Password);

            if (result.Successed)
            {
                return Ok(token);
            }
            
            return BadRequest(result.ToProblemDetails());
        }
    }
}