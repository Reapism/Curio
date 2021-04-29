using System;
using System.Threading;
using System.Threading.Tasks;
using Curio.Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

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

        public async Task SendEmailAsync(IEmailBuilder emailBuilder, CancellationToken cancellationToken = default)
        {
            var message = emailBuilder.Build();

            await SendEmailViaSmtpClientAsync(message, cancellationToken);
        }

        private async Task SendEmailViaSmtpClientAsync(MimeMessage message, CancellationToken cancellationToken = default)
        {
            using (var client = new SmtpClient())
            {
                //var clientCreds = GetClientCredentials();
                //var authCreds = GetAuthenticationCredentials();

                //client.Connect(clientCreds.Item1, clientCreds.Item2, clientCreds.Item3);

                //await client.AuthenticateAsync(authCreds.Item1, authCreds.Item2, cancellationToken);
                //await client.SendAsync(message, cancellationToken);
                
                //client.Disconnect(true);
            }
        }

        private void  SendEmailViaSmtpClient(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                //var clientCreds = GetClientCredentials();
                //var authCreds = GetAuthenticationCredentials();

                //client.Connect(clientCreds.Item1, clientCreds.Item2, clientCreds.Item3);
                
                //client.Authenticate(authCreds.Item1, authCreds.Item2);
                //client.Send(message);
                
                //client.Disconnect(true);
            }
        }

        //private (string, string) GetAuthenticationCredentials()
        //{
        //    var userName = configuration.GetSection("emailUserName").Value;
        //    var password = configuration.GetSection("emailPassword").Value;

        //    return (userName, password);
        //}

        //private (string, int, bool) GetClientCredentials()
        //{
        //    var host = configuration.GetSection("emailHost").Value;
        //    var port = int.Parse(configuration.GetSection("emailPort").Value);
        //    var useSsl = bool.Parse(configuration.GetSection("emailUseSsl").Value);

        //    return (host, port, useSsl);
        //}
    }
}
