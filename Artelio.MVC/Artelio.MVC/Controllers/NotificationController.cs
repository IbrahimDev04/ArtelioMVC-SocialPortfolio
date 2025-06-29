using System.Collections.Generic;
using System.Security.Claims;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Notification;
using Artelio.MVC.DTOs.Notification.Page;
using Artelio.MVC.Services.Implements;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class NotificationController(IAuthService _authService, INotificationService _notificationService, IMessageService _messageService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            await _notificationService.SeenNotification(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            List<GetUserNotificationDTO> notification = await _notificationService.GetUserNotification(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageNotificationDTO dto = new PageNotificationDTO
            {
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,
                notification = notification
            };




            return View(dto);
        }
    }
}
