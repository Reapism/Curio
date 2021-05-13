using System.Text.Json.Serialization;
using Curio.SharedKernel;
using Curio.SharedKernel.Authorization;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Home
{
    public sealed class AdministratorRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        public override string UserType { get; protected set; } = UserTypeConstants.Administrator;
    }

    public sealed class AdvertiserRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        public override string UserType { get; protected set; } = UserTypeConstants.Advertiser;
    }

    public sealed class EndUserRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        public override string UserType { get; protected set; } = UserTypeConstants.EndUser;
    }

    public sealed class InternalRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        public override string UserType { get; protected set; } = UserTypeConstants.Internal;
    }

    public abstract class RegistrationRequest
    {
        // Login details
        public string Email { get; init; }
        public string Password { get; init; }
        public string MobilePhone { get; init; }

        // Your information
        public string FirstName { get; init; }
        public string LastName { get; init; }

        // Profile 
        public string DisplayName { get; init; }
        public byte[] ImageBase64 { get; init; }

        public bool TwoFactorEnabled { get; init; }

        // Agreements
        public bool HasAgreedToEula { get; init; }
        public bool HasAgreedToPrivacyPolicy { get; init; }

        // Computed values from client side
        public bool HasMobilePhone { get; init; }
        public bool VerifyWithMobilePhone { get; init; }
        public bool VerifyWithEmail { get; init; }

        public bool IsFirstTimeLogin { get; init; } = true;

        [JsonIgnore]
        public virtual string UserType { get; protected set; } = UserTypeConstants.EndUser;
    }


    public class RegistrationResponse : ValidationResponse
    {

    }
}
