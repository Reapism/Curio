namespace Curio.Api.Exchanges.Home.Login
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
