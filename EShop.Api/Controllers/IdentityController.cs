using System.Threading.Tasks;
using EShop.Api.Helpers;
using EShop.Api.Models.Identity;
using EShop.Domain.Cache;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ICacheIdentityStorage _cacheIdentity;

        public IdentityController(IIdentityService identityService, ICacheIdentityStorage cacheIdentity)
        {
            _identityService = identityService;
            _cacheIdentity = cacheIdentity;
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
        public async Task<ActionResult> RegistrationAsync([FromBody] RegistrationViewModel model)
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
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody] LoginViewModel model)
        {
            var (result, loginResult) = await _identityService.LoginAsync(model.Email, model.Password);

            if (result.Successed)
            {
                return Ok(loginResult);
            }
            
            return BadRequest(result.ToProblemDetails());
        }

        /// <summary>
        /// Refresh jwt token
        /// </summary>
        /// <param name="model">refresh token</param>
        /// <response code="201">Successful refreshed</response>
        /// <response code="400">Smt went wrong</response>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResult>> RefreshTokenAsync([FromBody] RefreshViewModel model)
        {
            var (result, loginResult) = await _identityService.RefreshTokenAsync(model.RefreshToken);

            if (!result.Successed)
            {
                return BadRequest(result.ToProblemDetails());
            }

            return Ok(loginResult);
        }

        //TODO get cached value by key isnt work
        [HttpGet("check-cache/{key}")]
        public async Task<ActionResult> CheckCacheAsync([FromRoute] string key)
        {
            return Ok(await _cacheIdentity.GetCacheValueAsync(key));
        }
    }
}