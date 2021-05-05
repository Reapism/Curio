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
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }

        // Your information
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Profile 
        public string DisplayName { get; set; }
        public byte[] ImageBase64 { get; set; }

        public bool TwoFactorEnabled { get; set; }

        // Agreements
        public bool HasAgreedToEula { get; set; }
        public bool HasAgreedToPrivacyPolicy { get; set; }

        // Computed values from client side
        public bool HasMobilePhone { get; set; }
        public bool VerifyWithMobilePhone { get; set; }
        public bool VerifyWithEmail { get; set; }

        [JsonIgnore]
        public virtual string UserType { get; protected set; } = UserTypeConstants.EndUser;
    }


    public class RegistrationResponse : ValidationResponse
    {

    }
}
