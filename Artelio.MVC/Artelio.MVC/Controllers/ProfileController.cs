using System.Reflection;
using System.Security.Claims;
using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Friend;
using Artelio.MVC.DTOs.Profile;
using Artelio.MVC.DTOs.Profile.Page;
using Artelio.MVC.Entities;
using Artelio.MVC.Enums;
using Artelio.MVC.Extensions;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Artelio.MVC.Controllers
{
    [Authorize]
    public class ProfileController(IProfileSettingService _profileSettingService, IWebHostEnvironment _env, IAuthService _authService, IProfileService _profileService, IFriendService _friendService) : Controller
    {
        //Base Information
        [HttpGet]
        public async Task<IActionResult> Index()
            {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            UpdateBasicInfoDTO dto = await _profileSettingService.GetBasicInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            PageBasicInfoDTO baseDto = new PageBasicInfoDTO
            {
                UpdateBasicInfoDTO = dto,
                GetUserInfoDTO = userDto,
            };

            return View(baseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PageBasicInfoDTO baseDto)
        {
            var dto = baseDto.UpdateBasicInfoDTO;

            await _profileSettingService.UpdateBasicInfo(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, _env.WebRootPath);

            return View();
        }


        //Social Media
        [HttpGet]
        public async Task<IActionResult> SocialMedia()
        {

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UpdateSocialMediaInfoDTO socialDto = await _profileSettingService.GetSocialMediaInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageSocialMediaInfoDTO dto = new PageSocialMediaInfoDTO
            {
                GetUserInfoDTO = userDto,
                UpdateSocialMediaInfoDTO = socialDto,
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> SocialMedia(PageSocialMediaInfoDTO baseDto)
        {
            var dto = baseDto.UpdateSocialMediaInfoDTO;

            await _profileSettingService.UpdateSocialMediaInfo(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return View();
        }


        //Education
        [HttpGet]
        public async Task<IActionResult> Education()
        {
            List<GetEducationDTO> educationDTOs = await _profileSettingService.GetUserEducations(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageGetEducationDTO dto = new PageGetEducationDTO
            {
                educationDTOs = educationDTOs,
                GetUserInfoDTO = userDto
            };


            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEducation()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            CreateEducationDTO educationDTO = new CreateEducationDTO
            {
                Countries = Enum.GetValues(typeof(CountryEnum))
                      .Cast<CountryEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      }),
                Degrees = Enum.GetValues(typeof(DegreeEnum))
                      .Cast<DegreeEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      })
            };

            PageCreateEducationDTO dto = new PageCreateEducationDTO
            {
                CreateEducationDTO = educationDTO,
                GetUserInfoDTO = userDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducation(PageCreateEducationDTO baseDto)
        {
            CreateEducationDTO dto = baseDto.CreateEducationDTO;

            await _profileSettingService.CreateEducation(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("Education", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEducation(string education)
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UpdateUserEducationDTO userEducationDto = await _profileSettingService.GetUserEducation(education);

            PageUpdateEducationDTO dto = new PageUpdateEducationDTO
            {
                GetUserInfoDTO = userDto,
                UpdateUserEducationDTO = userEducationDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEducation(string education, PageUpdateEducationDTO baseDto)
        {
            UpdateUserEducationDTO dto = baseDto.UpdateUserEducationDTO;

            await _profileSettingService.UpdateUserEducationAsync(dto, education);

            return View();
        }

        public async Task<IActionResult> DeleteEducation(string education)
        {
            await _profileSettingService.DeleteUserEducationAsync(education);

            return RedirectToAction("Education", "Profile");
        }


        //Work Experience
        [HttpGet]
        public async Task<IActionResult> WorkExperience()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<GetUserWorkExperienceDTO> experiences = await _profileSettingService.GetWorkExperiences(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageGetWorkExperienceDTO dto = new PageGetWorkExperienceDTO
            {
                GetUserInfoDTO = userDto,
                GetUserWorkExperienceDTOs = experiences
            };

            return View(dto);  
        }

        [HttpGet]
        public async Task<IActionResult> CreateExperience()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            CreateUserExperienceDTO experienceDTO = new CreateUserExperienceDTO
            {
                Countries = Enum.GetValues(typeof(CountryEnum))
                      .Cast<CountryEnum>()
                      .Select(g => new SelectListItem
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      })
            };

            PageCreateExperienceDTO dto = new PageCreateExperienceDTO
            {
                GetUserInfoDTO = userDto,
                CreateUserExperience = experienceDTO
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExperience(PageCreateExperienceDTO baseDto)
        {
            CreateUserExperienceDTO dto = baseDto.CreateUserExperience;

            await _profileSettingService.CreateUserExperienceAsync(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("WorkExperience", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateExperience(string work)
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            UpdateUserExperienceDTO experienceDto = await _profileSettingService.GetWorkExperience(work);

            PageUpdateExperienceDTO dto = new PageUpdateExperienceDTO
            {
                GetUserInfoDTO = userDto,
                UpdateUserExperienceDTO = experienceDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExperience(string work, PageUpdateExperienceDTO dto)
        {
            UpdateUserExperienceDTO data = dto.UpdateUserExperienceDTO;

            await _profileSettingService.UpdateUserExperienceAsync(data, work);

            return RedirectToAction("WorkExperience", "Profile");
        }

        public async Task<IActionResult> DeleteExperience(string work)
        {
            await _profileSettingService.DeleteUserExperienceAsync(work);

            return RedirectToAction("WorkExperience", "Profile");
        }


        //Award
        public async Task<IActionResult> Award()
        {
            List<GetUserAwardDTO> awardDto = await _profileSettingService.GetUserAwards(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageGetAwardDTO dto = new PageGetAwardDTO
            {
                GetUserInfoDTO = userDto,
                GetUserAwardDTO = awardDto
            };

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAward()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageCreateAwardDTO dto = new PageCreateAwardDTO
            {
                GetUserInfoDTO = userDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAward(PageCreateAwardDTO baseDto)
        {
            CreateAwardDTO dto = baseDto.CreateAwardDTO;

            await _profileSettingService.CreateUserAward(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, _env.WebRootPath);

            return RedirectToAction("Award", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAward(string award)
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UpdateUserAwardDTO awardDto = await _profileSettingService.GetUserAward(award);

            PageUpdateAwardDTO dto = new PageUpdateAwardDTO
            {
                UpdateUserAwardDTO = awardDto,
                GetUserInfoDTO = userDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAward(string award, PageUpdateAwardDTO dto)
        {
            UpdateUserAwardDTO data = dto.UpdateUserAwardDTO;

            await _profileSettingService.UpdateUserAwardAsync(data, award, _env.WebRootPath);

            return RedirectToAction("Award", "Profile");
        }

        public async Task<IActionResult> DeleteAward(string award)
        {
            await _profileSettingService.DeleteUserAwardAsync(award);

            return RedirectToAction("Award", "Profile");
        }


        //Language
        public async Task<IActionResult> Language()
        {
            List<GetLanguageDTO> languageDto = await _profileSettingService.GetAllLanguages(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageGetLanguageDTO dto = new PageGetLanguageDTO
            {
                GetUserInfoDTO = userDto,
                Languages = languageDto
            };

            return View(dto);

        }

        [HttpGet]
        public async Task<IActionResult> CreateLanguage()
        {
            CreateLanguageDTO languageDto = new CreateLanguageDTO
            {
                Languages = Enum.GetValues(typeof(LanguageEnum))
                      .Cast<LanguageEnum>()
                      .Select(g => new SelectListItem   
                      {
                          Value = ((int)g).ToString(),
                          Text = g.ToString()
                      }),
                ExpertiseLevel = default
            };

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageCreateLanguageDTO dto = new PageCreateLanguageDTO
            {
                CreateLanguageDTO = languageDto,
                GetUserInfoDTO = userDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage(PageCreateLanguageDTO baseDto)
        {
            var levels = new[]
            {
                "Beginner", "Elementary", "Intermediate",
                "Upper Intermediate", "Advanced", "Proficient"
            };

            CreateLanguageDTO dto = baseDto.CreateLanguageDTO;

            await _profileSettingService.CreateUserLanguage(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, levels);

            return RedirectToAction("Language", "Profile");
        }

        public async Task<IActionResult> DeleteLanguage(string language)
        {
            await _profileSettingService.DeleteUserLanguageAsync(language);

            return RedirectToAction("Language", "Profile");
        }


        //Ability
        public async Task<IActionResult> Skills()
        {
            List<GetAbilityDTO> list = await _profileSettingService.GetAllAbility(User.FindFirst(ClaimTypes.NameIdentifier).Value); ;
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            PageGetAbilityDTO dto = new PageGetAbilityDTO
            {
                GetAbilityDTO = list,
                GetUserInfoDTO = userDto
            };

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSkills()
        {

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            CreateAbilityDTO abilityDto = new CreateAbilityDTO();

            PageCreateAbilityDTO dto = new PageCreateAbilityDTO
            {
                GetUserInfoDTO = userDto,
                CreateAbilityDTO = abilityDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkills([FromBody] PageCreateAbilityDTO baseDto)
        {
            CreateAbilityDTO dto = baseDto.CreateAbilityDTO;

            await _profileSettingService.CreateAbiliy(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("Skills", "Profile");
        }

        public async Task<IActionResult> DeleteSkills(string skill)
        {
            await _profileSettingService.DeleteUserAbilityAsync(skill);

            return RedirectToAction("skills", "profile");
        }


        //Project
        [HttpGet]
        public async Task<IActionResult> CreateProject()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            PageCreateProjectDTO dto = new PageCreateProjectDTO
            {
                GetUserInfoDTO = userDto
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(PageCreateProjectDTO baseDto)
        {
            CreateProjectDTO dto = baseDto.CreateProjectDTO;

            await _profileService.CreateProject(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, _env.WebRootPath);    

            return RedirectToAction("Index", "Home");
        }

        //Profile
        [HttpGet]
        public async Task<IActionResult> Profile(string? userId)
        {

            string UserId = userId == null ? User.FindFirst(ClaimTypes.NameIdentifier).Value : userId;

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserProfileDTO profileDto = await _authService.GetUserProfile(UserId);
            List<GetAbilityDTO> getAbilityDTOs = await _profileSettingService.GetAllAbility(UserId);
            List<GetLanguageDTO> getLanguageDTOs = await _profileSettingService.GetAllLanguages(UserId);
            List<GetUserAwardDTO> getUserAwardDTOs = await _profileSettingService.GetUserAwards(UserId);
            List<GetUserWorkExperienceDTO> getUserWorkExperienceDTOs = await _profileSettingService.GetWorkExperiences(UserId);
            List<GetEducationDTO> getUserEducationDTOs = await _profileSettingService.GetUserEducations(UserId);
            GetProfileSocialMediaDTO getProfileSocialMediaDTO = await _profileSettingService.GetSocialMediaProfile(UserId);
            GetProfileUserAboutDTO getProfileUserAboutDTO = await _authService.GetProfileUserAbout(UserId);
            int getAllViewer = await _profileService.GetUserViewerCount(UserId);
            int getAllProject = await _profileService.GetUserPostCount(UserId);
            bool? isTakeFriendRequest = userId != null ? await _friendService.IsTakeFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null;
            bool? isSendFriendRequest = userId != null ? await _friendService.IsSendFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null;
            bool? isFriendRequest = userId != null ? await _friendService.IsFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null;
            int friendCount = await _friendService.GetFriendCount(UserId);
            int friendRequestCount = await _friendService.GetAllFriendRequestCount(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            PageProfileDTO dto = new PageProfileDTO
            {
                GetUserInfoDTO = userDto,
                GetUserProfileDTO = profileDto,
                GetUserWorkExperienceDTOs = getUserWorkExperienceDTOs,
                GetUserAwardDTO = getUserAwardDTOs,
                GetAbilityDTO = getAbilityDTOs,
                Languages = getLanguageDTOs,
                GetProfileSocialMediaDTO = getProfileSocialMediaDTO,
                GetProfileUserAboutDTO = getProfileUserAboutDTO,
                getUserEducationDTOs = getUserEducationDTOs,
                GetAllViewer = getAllViewer,
                GetPostCount = getAllProject,
                isTakeFriendRequest = isTakeFriendRequest,
                isSendFriendRequest = isSendFriendRequest,
                isFriendRequest = isFriendRequest,
                GetFriendCount = friendCount/2,
                GetFriendRequestCount = friendRequestCount,
            };

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Friends(string? userId)
        {
            List<GetUserFriendsDTO> friendsDTOs = await _profileService.GetUserFriends(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return View(friendsDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Projects(string? userId)
        {
            string UserId = userId == null ? User.FindFirst(ClaimTypes.NameIdentifier).Value : userId;


            List<GetUserProjectsDTO> projectsDTOs = await _profileService.GetOnlyUserProjects(UserId);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserProfileDTO profileDto = await _authService.GetUserProfile(UserId);
            int getAllViewer = await _profileService.GetUserViewerCount(UserId);
            int getAllProject = await _profileService.GetUserPostCount(UserId);

            PageProfileProjectDTO dto = new PageProfileProjectDTO
            {
                projectsDTOs = projectsDTOs,
                GetUserInfoDTO = userDto,
                GetUserProfileDTO = profileDto,
                GetAllViewer = getAllViewer,
                GetPostCount = getAllProject,
            };

            return View(dto);  
        }

    }
}
