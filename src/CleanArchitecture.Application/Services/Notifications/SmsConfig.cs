namespace CleanArchitecture.Application.Services.Notifications
{
    public class SmsConfig : ISmsConfig
    {
        public string SmsServer { get; private set; }
    }
}