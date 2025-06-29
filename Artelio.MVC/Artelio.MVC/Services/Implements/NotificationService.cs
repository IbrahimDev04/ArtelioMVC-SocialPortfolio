using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Notification;
using Artelio.MVC.Entities;
using Artelio.MVC.Objects;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> GetActiveNotificationCount(string userId)
        {
            return await _context.notifications.CountAsync(u => (u.To == userId || _context.Follows.Any(a => a.FollowedId == u.UserId && a.FollowingId == userId && a.IsAccepted == true)) && u.isActive == true);
        }

        public async Task<List<GetUserNotificationDTO>> GetUserNotification(string userId)
        {
            List<GetUserNotificationDTO> dto = await _context.notifications.Where(u => (u.To == userId || _context.Follows.Any(a => a.FollowedId == u.UserId && a.FollowingId == userId && a.IsAccepted == true)))
                .OrderBy(u => u.Date)
                .Select(u => new GetUserNotificationDTO
                {
                    UserId = u.UserId,
                    UserImageUrl = u.User.ImageUrl,
                    UserName = u.User.UserName,
                    ContentAction = u.ContentAction,
                    ContentController = u.ContentController,
                    ContentId = u.ContentId,
                    ContentText = u.ContentText,
                    Date = _getTimeAgo(u.Date),
                    IsProject = u.ContentAction == "Detail"
                }).ToListAsync();

            return dto;
        }

        public async Task SeenNotification(string userId)
        {
            List<Notification> notifications = await _context.notifications.Where(u => (u.To == userId || _context.Follows.Any(a => a.FollowedId == u.UserId && a.FollowingId == userId && u.isActive == true)) && u.isActive == true).ToListAsync();


            foreach (var item in notifications)
            {
                item.isActive = false;

                await _context.SaveChangesAsync();
            }
        }

        private static string _getTimeAgo(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.UtcNow - dateTime.ToUniversalTime();

            if (timeSpan.TotalSeconds < 60)
                return $"{Math.Floor(timeSpan.TotalSeconds)} seconds ago";

            if (timeSpan.TotalMinutes < 60)
                return $"{Math.Floor(timeSpan.TotalMinutes)} minutes ago";

            if (timeSpan.TotalHours < 24)
                return $"{Math.Floor(timeSpan.TotalHours)} hours ago";

            if (timeSpan.TotalDays < 30)
                return $"{Math.Floor(timeSpan.TotalDays)} days ago";

            if (timeSpan.TotalDays < 365)
                return $"{Math.Floor(timeSpan.TotalDays / 30)} months ago";

            return $"{Math.Floor(timeSpan.TotalDays / 365)} years ago";
        }
    }
}
