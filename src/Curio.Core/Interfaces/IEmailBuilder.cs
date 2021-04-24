using MimeKit;

namespace Curio.Core.Interfaces
{
    public interface IEmailBuilder
    {
        IEmailBuilder AddFromAddress(params string[] fromAddresses);
        IEmailBuilder AddToAddress(params string[] toAddresses);
        IEmailBuilder AddBccAddress(params string[] bccAddresses);
        IEmailBuilder AddCcAddress(params string[] ccAddresses);
        IEmailBuilder AddAttachment();

        IEmailBuilder SetPriorityUrgent();
        IEmailBuilder SetSubject(string subject);
        IEmailBuilder SetTextBody(string content);
        IEmailBuilder SetHtmlBody(string content);

        MimeMessage Build();
    }
}
