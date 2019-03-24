namespace CleanArchitecture.Application.Services
{
    public interface INotificationService
    {
        Task SendAsync(string message, string userId);
    }
}
