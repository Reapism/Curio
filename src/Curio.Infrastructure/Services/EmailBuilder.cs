using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Curio.Core.Extensions;
using Curio.Core.Interfaces;
using Curio.SharedKernel.Bases;
using MimeKit;

namespace Curio.Infrastructure.Services
{
    public class EmailBuilder : IEmailBuilder
    {
        private MimeMessage mimeMessage;
        private Multipart bodyMultiPartCollection;
        private BodyBuilder bodyBuilder;
        public EmailBuilder()
        {
            mimeMessage = new MimeMessage();
            bodyMultiPartCollection = new Multipart("mixed");
            bodyBuilder = new BodyBuilder();
        }

        public IEmailBuilder AddAttachment()
        {
            var attachment = new MimePart()
            {
                IsAttachment = true,
            };

            bodyBuilder.Attachments.Add(attachment);
            return this;
        }

        public IEmailBuilder AddBccAddress(params string[] bccAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var bccAddress in bccAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", bccAddress));
            }

            mimeMessage.Bcc.AddRange(internetAddresses);
            return this;
        }

        public IEmailBuilder AddCcAddress(params string[] ccAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var ccAddress in ccAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", ccAddress));
            }

            mimeMessage.Cc.AddRange(internetAddresses);
            return this;
        }

        public IEmailBuilder AddFromAddress(params string[] fromAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var fromAddress in fromAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", fromAddress));
            }

            mimeMessage.From.AddRange(internetAddresses);
            return this;
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
            Guard.Against.NullOrWhiteSpace(mimeMessage.Subject, nameof(mimeMessage.Subject));
            Guard.Against.Null(mimeMessage.From, nameof(mimeMessage.From));
            Guard.Against.Null(mimeMessage.To, nameof(mimeMessage.To));
            Guard.Against.Null(mimeMessage.Body, nameof(mimeMessage.Body));
            Guard.Against.Null(mimeMessage.Priority, nameof(mimeMessage.Priority));

            mimeMessage.Date = DateTimeOffset.UtcNow;
            mimeMessage.Priority = MessagePriority.Normal;

            return this;
        }

        public IEmailBuilder SetBody(string content)
        {
            Guard.Against.NullOrWhiteSpace(content, nameof(content));

            var contentBytes = content.ToUtf8Bytes();
            var memoryStream = new MemoryStream(contentBytes);
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Content = new MimeContent(memoryStream),
                ContentTransferEncoding = ContentEncoding.Base64,
            };

            return this;
        }

        public IEmailBuilder SetPriorityUrgent()
        {
            mimeMessage.Priority = MessagePriority.Urgent;

            return this;
        }

        public IEmailBuilder SetSubject(string subject)
        {
            Guard.Against.NullOrWhiteSpace(subject, nameof(subject));

            var sanitizedSubject = Sanitize(subject);
            mimeMessage.Subject = sanitizedSubject;

            return this;
        }

        private string Sanitize(string input)
        {
            var inputTrimmed = input.Trim();
            var normalizedInput = inputTrimmed.Normalize();

            return normalizedInput;
        }
    }
}
