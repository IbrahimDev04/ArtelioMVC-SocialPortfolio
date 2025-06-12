using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Notification;
using Artelio.MVC.Entities;
using Artelio.MVC.Objects;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Artelio.MVC.Services.Implements
{
    public class NotificationService(ArtelioContext _context) : INotificationService
    {
        public async Task CreateNotification(string userId, NotificationObject @object)
        {
            Notification notification = new Notification
            {
                UserId = userId,
                isActive = true,
                ContentId = @object.ContentId,
                ContentAction = @object.ContentAction,
                ContentController = @object.ContentController,
                ContentText = @object.ContentText,
                To = @object.To,
            };

            await _context.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public Task<List<GetUserNotificationDTO>> GetUserNotification(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
