namespace Curio.WebApi.Exchanges.Home
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool IsFailure { get; set; }
        public string FailureMessage { get; set; }
    }
}
