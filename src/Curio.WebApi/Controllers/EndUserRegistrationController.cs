using System.Threading;
using System.Threading.Tasks;
using Curio.WebApi.Exchanges.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Controllers
{
    public class EndUserRegistrationController : CurioControllerBase
    {
        public EndUserRegistrationController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Registers an end user account used for authentication.
        /// </summary>
        /// <param name="endUserRegistrationRequest">The request body</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /RegisterEndUser
        ///     {
        ///        "email": "example@example.com",
        ///        "password": "password",
        ///        "mobilePhone": 1112223333,
        ///        "firstName": "Anthony",
        ///        "lastName": "Doe",
        ///        "displayName": "iReapism",
        ///        "twoFactorEnabled": true,
        ///        "hasAgreedToEula": true,
        ///        "hasAgreedToPrivacyPolicy": true,
        ///        "hasMobilePhone": true,
        ///        "verifyWithMobilePhone": true,
        ///        "verifyWithEmail": true,
        ///        "hasAgreedToPrivacyPolicy": true,
        ///     }
        ///
        /// </remarks>
        /// <returns>A standard</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterEndUser([FromForm] EndUserRegistrationRequest endUserRegistrationRequest, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(endUserRegistrationRequest, cancellationToken);
            return Ok(result); // TODO use new extension method that will help return this based on ApiResponse status code.
        }
    }
}
