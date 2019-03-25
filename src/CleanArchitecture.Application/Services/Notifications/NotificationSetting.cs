namespace CleanArchitecture.Application.Services.Notifications
{
    public class NotificationSetting : INotificationSetting
    {
        public ISmsConfig SmsConfig { get; set; }
        public IEmailConfig EmailConfig { get; set; }
    }
}