using System;
using System.Collections.Generic;
using System.Text;
using Curio.Core.Interfaces;
using MimeKit;

namespace Curio.Infrastructure.Services
{
    public class EmailBuilder : IEmailBuilder
    {
        private MimeMessage mimeMessage;
        public EmailBuilder()
        {
            mimeMessage = new MimeMessage();
        }

        public IEmailBuilder AddAttachment()
        {
            throw new NotImplementedException();
        }

        public IEmailBuilder AddBccAddress(params string[] bccAddresses)
        {
            throw new NotImplementedException();
        }

        public IEmailBuilder AddCcAddress(params string[] ccAddresses)
        {
            throw new NotImplementedException();
        }

        public IEmailBuilder AddFromAddress(params string[] fromAddresses)
        {
            throw new NotImplementedException();
        }

        public IEmailBuilder AddToAddress(params string[] toAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var toAddress in toAddresses)
            {

                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", toAddress));
            }

            mimeMessage.To.AddRange(internetAddresses);
            return this;
        }

        public IEmailBuilder Build()
        {
            throw new NotImplementedException();
        }

        public IEmailBuilder SetBody(string content)
        {
            throw new NotImplementedException();
        }

        public IEmailBuilder SetSubject(string subject)
        {
            throw new NotImplementedException();
        }
    }
}
