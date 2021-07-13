using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Curio.ApplicationCore.Interfaces;
using Curio.Domain.Entities;
using Curio.Domain.Extensions;
using Curio.Infrastructure.Services.Identity;
using Curio.Persistence.Identity;
using Curio.SharedKernel;
using Curio.SharedKernel.Interfaces;
using Curio.WebApi.Exchanges.Identity;
using Curio.WebApi.Extensions;
using Curio.WebApi.Handlers.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Curio.WebApi.Controllers
{
    public class EndUserRegistrationController : ApiControllerBase
    {
        private readonly IComponentContext container;

        public EndUserRegistrationController(IMediator mediator, IComponentContext container) : base(mediator)
        {
            this.container = container;
            var a = container.IsRegistered<IUserRegistrationService<EndUserRegistrationRequest>>();
            var b = container.IsRegistered<IAppLogger<UserRegistrationService<EndUserRegistrationRequest>>>();
            var c = container.IsRegistered<UserManager<ApplicationUser>>();
            var d = container.IsRegistered<IPasswordHasher<ApplicationUser>>();
            var e = container.IsRegistered<IHashingService>();
            var f = container.IsRegistered<IRepository<UserProfile>>();
            var g = container.IsRegistered<IRequestHandler<EndUserRegistrationRequest, ApiResponse<RegistrationResponse>>>();
            var h = container.IsRegistered<EndUserRegistrationHandler>();
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
        ///        "displayName": "Reap",
        ///        "uniqueHandle": "iReapism",
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
            return result.FromApiResponse();
        }
    }
}
