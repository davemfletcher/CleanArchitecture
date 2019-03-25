using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services.Notifications
{
    public interface INotificationService
    {
        Task<bool> SendAsync(string message, string userId);
    }
}
