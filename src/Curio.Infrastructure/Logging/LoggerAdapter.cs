using Curio.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace Curio.Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
            logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            logger.LogInformation(message, args);
        }
    }
}
