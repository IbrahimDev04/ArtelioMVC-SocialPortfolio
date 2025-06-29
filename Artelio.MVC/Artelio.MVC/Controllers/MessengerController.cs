using System.Security.Claims;
using System.Threading.Tasks;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Messenger;
using Artelio.MVC.DTOs.Messenger.Page;
using Artelio.MVC.Services.Implements;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class MessengerController(IFriendService _friendService, IAuthService _authService, IMessageService _messageService, INotificationService _notificationService) : Controller
    {
        public async Task<IActionResult> Index(string? recieveUser)
        {
            List<GetUserFriendsForMessengerDTO> friends = await _friendService.GetUserFriends(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<GetMessageDTO> messages = recieveUser != null ? await _messageService.GetCurrentUserMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value, recieveUser) : null;
            GetUserInfoDTO GetUserInfoDTO = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO GetRecieveUserDTO = recieveUser != null ? await _authService.GetUserInfo(recieveUser) : null;
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageGetMessengerDTO dto = new PageGetMessengerDTO
            {
                Friends = friends,
                GetUserInfoDTO = GetUserInfoDTO,
                CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                RecieveUserId = recieveUser,
                GetRecieveUserDTO = GetRecieveUserDTO,
                Messages = messages,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,
            };

            return View(dto);
        }
    }
}
