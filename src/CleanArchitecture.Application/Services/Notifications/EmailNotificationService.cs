using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Services.Notifications
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly IEmailConfig _setting;

        public EmailNotificationService(ILogger<EmailNotificationService> logger, IEmailConfig setting)
        {
            _logger = logger;
            _setting = setting;
        }

        public Task<bool> SendAsync(string message, string userId)
        {
            // imagine sending logic via an external service

            _logger.LogInformation($"Sending email notification to user '{userId}'.");
            return Task.FromResult(true);
        }
    }
}