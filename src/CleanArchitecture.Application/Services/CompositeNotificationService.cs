using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace CleanArchitecture.Application.Services
{
    public class CompositeNotificationService : INotificationService
    {
        private readonly IEnumerable<INotificationService> _notificationServices;

        public CompositeNotificationService(IEnumerable<INotificationService> notificationServices)
        {
            _notificationServices = notificationServices;
        }

        public async Task SendAsync(string message, string userId)
        {
            foreach (var notificationService in _notificationServices)
            {
                await notificationService.SendAsync(message, userId);
            }
        }
    }

    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly IEmailConfig _setting;

        public EmailNotificationService(ILogger<EmailNotificationService> logger, IEmailConfig setting)
        {
            _logger = logger;
            _setting = setting;
        }

        public Task SendAsync(string message, string userId)
        {
            // imagine sending logic via an external service

            _logger.LogInformation($"Sending email notification to user '{userId}'.");
            return Task.CompletedTask;
        }
    }

    public interface IEmailConfig
    {
        string EmailServer { get; }
    }

    public class EmailConfig : IEmailConfig
    {
        public string EmailServer { get; private set; }

    }

    public interface ISmsConfig
    {
        string SmsServer { get; }
    }

    public class SmsConfig : ISmsConfig
    {
        public string SmsServer { get; private set; }
    }

    public interface INotificationSetting
    {
        ISmsConfig SmsConfig { get; set; }
        IEmailConfig EmailConfig { get; set; }
    }

    public class NotificationSetting : INotificationSetting
    {
        public ISmsConfig SmsConfig { get; set; }
        public IEmailConfig EmailConfig { get; set; }
    }
}
