using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;
using Curio.Core.Events;
using Curio.SharedKernel.Bases;
using Curio.SharedKernel.Interfaces;

namespace Curio.ApplicationCore.Services
{
    public class ItemCompletedEmailNotificationHandler : IHandle<ToDoItemCompletedEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMimeMessageBuilder emailBuilder;

        public ItemCompletedEmailNotificationHandler(IEmailSender emailSender, IMimeMessageBuilder emailBuilder)
        {
            _emailSender = emailSender;
            this.emailBuilder = emailBuilder;
        }

        // configure a test email server to demo this works
        // https://ardalis.com/configuring-a-local-test-email-server
        public async Task Handle(ToDoItemCompletedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));
            emailBuilder.AddFromAddress("test@mailinator.com");
            emailBuilder.AddToAddress("test@mailinator.com");
            emailBuilder.SetSubject($"{domainEvent.CompletedItem.Title} was completed.");
            emailBuilder.SetTextBody(domainEvent.CompletedItem.ToString());
            await _emailSender.SendEmailAsync(emailBuilder);
        }
    }
}
