namespace Curio.Core.Interfaces
{
    public interface IEmailValidator
    {
        bool IsValidEmail(string email);
        string GetEmailName(string email);
    }
}
