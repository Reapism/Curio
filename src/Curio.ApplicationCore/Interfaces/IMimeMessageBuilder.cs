using MimeKit;

namespace Curio.ApplicationCore.Interfaces
{
    public interface IMimeMessageBuilder
    {
        IMimeMessageBuilder AddFromAddress(params string[] fromAddresses);
        IMimeMessageBuilder AddToAddress(params string[] toAddresses);
        IMimeMessageBuilder AddBccAddress(params string[] bccAddresses);
        IMimeMessageBuilder AddCcAddress(params string[] ccAddresses);
        IMimeMessageBuilder AddAttachment();

        IMimeMessageBuilder SetPriorityUrgent();
        IMimeMessageBuilder SetSubject(string subject);
        IMimeMessageBuilder SetTextBody(string content);
        IMimeMessageBuilder SetHtmlBody(string content);

        MimeMessage Build();
    }
}
