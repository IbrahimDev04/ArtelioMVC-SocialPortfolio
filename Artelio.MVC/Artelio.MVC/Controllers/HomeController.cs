using System.Security.Claims;
using System.Threading.Tasks;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Profile;
using Artelio.MVC.DTOs.Profile.Page;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class HomeController(IAuthService _authService, IProfileService _profileService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<GetAllProject> projectDto = await _profileService.GetAllProject();
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            List<GetAllUserProjectDTO> project = await _profileService.GetUserProjects(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            PageHomeDTO dto = new PageHomeDTO
            {
                getAllProjects = projectDto,
                GetUserInfoDTO = userDto,
                getAllUserProjectDTOs = project
            }; 

            return View(dto);
        }

        public async Task<IActionResult> Detail(string project)
        {
            GetProjectOfUserDTO projectDTO = await _profileService.GetProjectOfUser(project);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            List<GetAllUserProjectDTO> projects = await _profileService.GetUserProjects(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            PageDetailDTO dto = new PageDetailDTO
            {
                GetProjectDTO = projectDTO,
                GetUserInfoDTO = userDto,
                getAllUserProjectDTOs = projects
            };

            if(!await _profileService.CheckViewer(project, User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
            {
                await _profileService.ViewProject(project, User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            }

            return View(dto);
        }
    }
}
