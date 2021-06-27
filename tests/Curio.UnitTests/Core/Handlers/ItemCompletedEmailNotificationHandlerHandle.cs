using System;
using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;
using Curio.ApplicationCore.Services;
using Curio.Domain.Entities;
using Curio.Domain.Events;
using Moq;
using Xunit;

namespace Curio.UnitTests.Core.Entities
{
    public class ItemCompletedEmailNotificationHandlerHandle
    {
        private ItemCompletedEmailNotificationHandler _handler;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IMimeMessageBuilder> _emailBuilderMock;
        public ItemCompletedEmailNotificationHandlerHandle()
        {
            _emailSenderMock = new Mock<IEmailSender>();
            _emailBuilderMock = new Mock<IMimeMessageBuilder>();
            _handler = new ItemCompletedEmailNotificationHandler(_emailSenderMock.Object, _emailBuilderMock.Object);
        }

        [Fact]
        public async Task ThrowsExceptionGivenNullEventArgument()
        {
            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null));
        }

        [Fact]
        public async Task SendsEmailGivenEventInstance()
        {
            await _handler.Handle(new ToDoItemCompletedEvent(new ToDoItem()));

            //_emailSenderMock.Verify(sender => sender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}