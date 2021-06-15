﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Curio.SharedKernel;
using Curio.SharedKernel.Authorization;
using Curio.SharedKernel.Bases;
using MediatR;

namespace Curio.WebApi.Exchanges.Identity
{
    public sealed class AdministratorRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        [JsonIgnore]
        public override string UserType { get; protected set; } = UserTypeConstants.Administrator;
    }

    public sealed class AdvertiserRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        [JsonIgnore]
        public override string UserType { get; protected set; } = UserTypeConstants.Advertiser;
    }

    public sealed class EndUserRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        [JsonIgnore]
        public override string UserType { get; protected set; } = UserTypeConstants.EndUser;
    }

    public sealed class InternalRegistrationRequest : RegistrationRequest, IRequest<ApiResponse<RegistrationResponse>>
    {
        [JsonIgnore]
        public override string UserType { get; protected set; } = UserTypeConstants.Internal;
    }

    public abstract class RegistrationRequest
    {
        // Login details
        [Required]
        [EmailAddress]
        // TODO: Might not need the ui attributes for requests. Well see. Might use different page models to construct a RegistrationRequest
        // these attributes might be inheritted to the sealed classes above.
        [DisplayName("Email Address")]
        [Description("The email address associated with the user used for authentication")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Phone]
        [DisplayName("Mobile Phone")]
        public string MobilePhone { get; set; }

        // Your information
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        // Profile 

        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string UniqueHandle { get; set; }
        public byte[] ImageBase64 { get; set; }

        public bool TwoFactorEnabled { get; set; }

        // Agreements
        [DefaultValue(false)]
        [DisplayName("Agree To EULA")]
        public bool HasAgreedToEula { get; set; }
        [DefaultValue(false)]
        [DisplayName("Agree To Privacy Policy")]
        public bool HasAgreedToPrivacyPolicy { get; set; }

        // Computed values from client side
        public bool HasMobilePhone { get; set; }
        public bool VerifyWithMobilePhone { get; set; }
        public bool VerifyWithEmail { get; set; }

        public bool IsFirstTimeLogin { get; set; } = true;

        [JsonIgnore]
        public virtual string UserType { get; protected set; } = UserTypeConstants.EndUser;
    }


    public class RegistrationResponse : ValidationResponse
    {

    }
}
