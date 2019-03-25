namespace CleanArchitecture.Application.Services.Notifications
{
    public interface INotificationSetting
    {
        ISmsConfig SmsConfig { get; set; }
        IEmailConfig EmailConfig { get; set; }
    }
}