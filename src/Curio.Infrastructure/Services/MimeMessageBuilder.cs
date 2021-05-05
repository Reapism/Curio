using System;
using System.Collections.Generic;
using System.Text;
using Curio.Core.Interfaces;
using Curio.SharedKernel.Bases;
using MimeKit;

namespace Curio.Infrastructure.Services
{
    public class MimeMessageBuilder : IMimeMessageBuilder
    {
        private MimeMessage mimeMessage;
        private BodyBuilder bodyBuilder;

        public MimeMessageBuilder()
        {
            mimeMessage = new MimeMessage();
            bodyBuilder = new BodyBuilder();
        }

        public IMimeMessageBuilder AddAttachment()
        {
            var attachment = new MimePart()
            {
                IsAttachment = true,
            };

            bodyBuilder.Attachments.Add(attachment);
            return this;
        }

        public IMimeMessageBuilder AddBccAddress(params string[] bccAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var bccAddress in bccAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", bccAddress));
            }

            mimeMessage.Bcc.AddRange(internetAddresses);
            return this;
        }

        public IMimeMessageBuilder AddCcAddress(params string[] ccAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var ccAddress in ccAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", ccAddress));
            }

            mimeMessage.Cc.AddRange(internetAddresses);
            return this;
        }

        public IMimeMessageBuilder AddFromAddress(params string[] fromAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var fromAddress in fromAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", fromAddress));
            }

            mimeMessage.From.AddRange(internetAddresses);
            return this;
        }

        public IMimeMessageBuilder AddToAddress(params string[] toAddresses)
        {
            var internetAddresses = new Queue<InternetAddress>();
            foreach (var toAddress in toAddresses)
            {
                internetAddresses.Enqueue(new MailboxAddress(Encoding.UTF8, "", toAddress));
            }

            mimeMessage.To.AddRange(internetAddresses);
            return this;
        }

        /// <summary>
        /// Validates the required data in order to utilize this <see cref="MimeMessage"/> and
        /// then builds a <see cref="MimeMessage"/>.
        /// <para>
        /// Validates that there is a From, To, Body and Subject.
        /// </para>
        /// </summary>
        /// <returns></returns>
        public MimeMessage Build()
        {
            // Guards before building.
            Guard.Against.NullOrWhiteSpace(mimeMessage.Subject, nameof(mimeMessage.Subject));
            Guard.Against.NullOrEmpty(mimeMessage.From, nameof(mimeMessage.From));
            Guard.Against.NullOrEmpty(mimeMessage.To, nameof(mimeMessage.To));
            Guard.Against.Null(mimeMessage.Body, nameof(mimeMessage.Body));
            Guard.Against.Null(mimeMessage.Priority, nameof(mimeMessage.Priority));

            mimeMessage.Date = DateTimeOffset.UtcNow;
            mimeMessage.Priority = MessagePriority.Normal;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            // TODO Look into below. Maybe necessary for images embedded in SetHtmlBody.
            // bodyBuilder.LinkedResources
            return mimeMessage;
        }

        public IMimeMessageBuilder SetTextBody(string textContent)
        {
            Guard.Against.NullOrWhiteSpace(textContent, nameof(textContent));

            // Sanitize the text body.
            var sanitizedTextBody = Sanitize(textContent);
            bodyBuilder.TextBody = sanitizedTextBody;

            return this;
        }

        public IMimeMessageBuilder SetHtmlBody(string htmlContent)
        {
            Guard.Against.NullOrWhiteSpace(htmlContent, nameof(htmlContent));

            // Sanitize the html body.
            //var sanitizedHtmlBody = Sanitize(htmlContent);
            bodyBuilder.HtmlBody = htmlContent;
            return this;
        }

        public IMimeMessageBuilder SetPriorityUrgent()
        {
            mimeMessage.Priority = MessagePriority.Urgent;

            return this;
        }

        public IMimeMessageBuilder SetSubject(string subject)
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
