using Artelio.MVC.DTOs.Notification;
using Artelio.MVC.Objects;

namespace Artelio.MVC.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotification(string userId, NotificationObject @object);
        Task<List<GetUserNotificationDTO>> GetUserNotification(string userId);
    }
}
