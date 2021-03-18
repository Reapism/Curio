using Curio.Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Threading.Tasks;

namespace Curio.Infrastructure
{
    // https://github.com/jstedfast/MailKit#using-mailkit
    // TODO: Need to build Builders for emailing html templates easy.
    // Maybe use dotliquid or build html for the emails.
    // Determine if email client is smpt, pop3, or imap
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            this.logger = logger;
        }

        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            await SendEmailAsyncInternal(to, from, subject, body);
        }

        private async Task SendEmailAsyncInternal(string to, string from, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Default", from));
            message.To.Add(new MailboxAddress("Hello", to));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };


            using (var client = new SmtpClient())
            {
                client.Connect("smpt.example.com", 587, false);
                await client.AuthenticateAsync("username", "password");
                await client.SendAsync(message);
                client.Disconnect(true);
            }
            logger.LogWarning($"Sending email to {to} from {from} with subject {subject}.");
        }
    }
}
