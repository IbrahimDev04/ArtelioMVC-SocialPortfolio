using System.Security.Claims;
using System.Threading.Tasks;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Profile;
using Artelio.MVC.DTOs.Profile.Page;
using Artelio.MVC.Services.Implements;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class HomeController(IAuthService _authService, IProfileService _profileService, INotificationService _notificationService, IMessageService _messageService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetAllProject> projectDto = await _profileService.GetAllProject();
            GetUserInfoDTO? userDto = User.Identity.IsAuthenticated ? await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) : new GetUserInfoDTO();
            List<GetAllUserProjectDTO> project = User.Identity.IsAuthenticated ? await _profileService.GetUserProjects(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) : new List<GetAllUserProjectDTO>();
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;



            PageHomeDTO dto = new PageHomeDTO
            {
                getAllProjects = projectDto,
                GetUserInfoDTO = userDto,
                getAllUserProjectDTOs = project,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,
            }; 

            return View(dto);
        }

        public async Task<IActionResult> Detail(string project)
        {
            GetProjectOfUserDTO projectDTO = await _profileService.GetProjectOfUser(project);
            GetUserInfoDTO userDto = User.Identity.IsAuthenticated ? await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) : new GetUserInfoDTO();
            List<GetAllUserProjectDTO> projects = User.Identity.IsAuthenticated ? await _profileService.GetUserProjects(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) : new List<GetAllUserProjectDTO>();
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;



            PageDetailDTO dto = new PageDetailDTO
            {
                GetProjectDTO = projectDTO,
                GetUserInfoDTO = userDto,
                getAllUserProjectDTOs = projects,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,
            };

            if (User.Identity.IsAuthenticated)
            {
                if (!await _profileService.CheckViewer(project, User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                {
                    await _profileService.ViewProject(project, User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                }
            }

            return View(dto);
        }
    }
}
