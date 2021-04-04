using Curio.SharedKernel.Interfaces;

namespace Curio.Core.Models
{
    public class RegistrationModel : IModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string DisplayName { get; set; }
    }

    public class RegistrationResponse : IModel
    {

    }
}
