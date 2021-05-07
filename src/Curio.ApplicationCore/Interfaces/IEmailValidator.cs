namespace Curio.ApplicationCore.Interfaces
{
    public interface IEmailValidator
    {
        bool IsValidEmail(string email);
        string GetEmailName(string email);
    }
}
