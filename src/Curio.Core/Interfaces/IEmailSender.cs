using System.Threading;
using System.Threading.Tasks;

namespace Curio.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(IMimeMessageBuilder emailBuilder, CancellationToken cancellationToken = default);
    }
}
