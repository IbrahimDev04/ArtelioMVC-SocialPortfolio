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

    public class ProfileController(IProfileSettingService _profileSettingService, IWebHostEnvironment _env, IAuthService _authService, IProfileService _profileService, IFriendService _friendService, INotificationService _notificationService, IMessageService _messageService) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
            {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            UpdateBasicInfoDTO dto = await _profileSettingService.GetBasicInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageBasicInfoDTO baseDto = new PageBasicInfoDTO
            {
                UpdateBasicInfoDTO = dto,
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,
            };

            return View(baseDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(PageBasicInfoDTO baseDto)
        {
            var dto = baseDto.UpdateBasicInfoDTO;

            await _profileSettingService.UpdateBasicInfo(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, _env.WebRootPath);

            return RedirectToAction("Profile", "Profile");
        }


        //Social Media
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SocialMedia()
        {

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UpdateSocialMediaInfoDTO socialDto = await _profileSettingService.GetSocialMediaInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageSocialMediaInfoDTO dto = new PageSocialMediaInfoDTO
            {
                GetUserInfoDTO = userDto,
                UpdateSocialMediaInfoDTO = socialDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SocialMedia(PageSocialMediaInfoDTO baseDto)
        {
            var dto = baseDto.UpdateSocialMediaInfoDTO;

            await _profileSettingService.UpdateSocialMediaInfo(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("Profile", "Profile");
        }


        //Education
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Education()
        {
            List<GetEducationDTO> educationDTOs = await _profileSettingService.GetUserEducations(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageGetEducationDTO dto = new PageGetEducationDTO
            {
                educationDTOs = educationDTOs,
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };


            return View(dto);
        }

        [Authorize]
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
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageCreateEducationDTO dto = new PageCreateEducationDTO
            {
                CreateEducationDTO = educationDTO,
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEducation(PageCreateEducationDTO baseDto)
        {
            CreateEducationDTO dto = baseDto.CreateEducationDTO;

            await _profileSettingService.CreateEducation(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("Education", "Profile");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateEducation(string education)
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UpdateUserEducationDTO userEducationDto = await _profileSettingService.GetUserEducation(education);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageUpdateEducationDTO dto = new PageUpdateEducationDTO
            {
                GetUserInfoDTO = userDto,
                UpdateUserEducationDTO = userEducationDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateEducation(string education, PageUpdateEducationDTO baseDto)
        {
            UpdateUserEducationDTO dto = baseDto.UpdateUserEducationDTO;

            await _profileSettingService.UpdateUserEducationAsync(dto, education);

            return RedirectToAction("Education", "Profile");
        }

        [Authorize]
        public async Task<IActionResult> DeleteEducation(string education)
        {
            await _profileSettingService.DeleteUserEducationAsync(education);

            return RedirectToAction("Education", "Profile");
        }


        //Work Experience
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> WorkExperience()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<GetUserWorkExperienceDTO> experiences = await _profileSettingService.GetWorkExperiences(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageGetWorkExperienceDTO dto = new PageGetWorkExperienceDTO
            {
                GetUserInfoDTO = userDto,
                GetUserWorkExperienceDTOs = experiences,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);  
        }

        [Authorize]
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
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageCreateExperienceDTO dto = new PageCreateExperienceDTO
            {
                GetUserInfoDTO = userDto,
                CreateUserExperience = experienceDTO,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateExperience(PageCreateExperienceDTO baseDto)
        {
            CreateUserExperienceDTO dto = baseDto.CreateUserExperience;

            await _profileSettingService.CreateUserExperienceAsync(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("WorkExperience", "Profile");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateExperience(string work)
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            UpdateUserExperienceDTO experienceDto = await _profileSettingService.GetWorkExperience(work);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageUpdateExperienceDTO dto = new PageUpdateExperienceDTO
            {
                GetUserInfoDTO = userDto,
                UpdateUserExperienceDTO = experienceDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateExperience(string work, PageUpdateExperienceDTO dto)
        {
            UpdateUserExperienceDTO data = dto.UpdateUserExperienceDTO;

            await _profileSettingService.UpdateUserExperienceAsync(data, work);

            return RedirectToAction("WorkExperience", "Profile");
        }

        [Authorize]
        public async Task<IActionResult> DeleteExperience(string work)
        {
            await _profileSettingService.DeleteUserExperienceAsync(work);

            return RedirectToAction("WorkExperience", "Profile");
        }


        //Award
        [Authorize]
        public async Task<IActionResult> Award()
        {
            List<GetUserAwardDTO> awardDto = await _profileSettingService.GetUserAwards(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageGetAwardDTO dto = new PageGetAwardDTO
            {
                GetUserInfoDTO = userDto,
                GetUserAwardDTO = awardDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateAward()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageCreateAwardDTO dto = new PageCreateAwardDTO
            {
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAward(PageCreateAwardDTO baseDto)
        {
            CreateAwardDTO dto = baseDto.CreateAwardDTO;

            await _profileSettingService.CreateUserAward(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, _env.WebRootPath);

            return RedirectToAction("Award", "Profile");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateAward(string award)
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UpdateUserAwardDTO awardDto = await _profileSettingService.GetUserAward(award);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageUpdateAwardDTO dto = new PageUpdateAwardDTO
            {
                UpdateUserAwardDTO = awardDto,
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAward(string award, PageUpdateAwardDTO dto)
        {
            UpdateUserAwardDTO data = dto.UpdateUserAwardDTO;

            await _profileSettingService.UpdateUserAwardAsync(data, award, _env.WebRootPath);

            return RedirectToAction("Award", "Profile");
        }

        [Authorize]
        public async Task<IActionResult> DeleteAward(string award)
        {
            await _profileSettingService.DeleteUserAwardAsync(award);

            return RedirectToAction("Award", "Profile");
        }


        //Language
        [Authorize]
        public async Task<IActionResult> Language()
        {
            List<GetLanguageDTO> languageDto = await _profileSettingService.GetAllLanguages(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageGetLanguageDTO dto = new PageGetLanguageDTO
            {
                GetUserInfoDTO = userDto,
                Languages = languageDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);

        }

        [Authorize]
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
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageCreateLanguageDTO dto = new PageCreateLanguageDTO
            {
                CreateLanguageDTO = languageDto,
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> DeleteLanguage(string language)
        {
            await _profileSettingService.DeleteUserLanguageAsync(language);

            return RedirectToAction("Language", "Profile");
        }


        //Ability
        [Authorize]
        public async Task<IActionResult> Skills()
        {
            List<GetAbilityDTO> list = await _profileSettingService.GetAllAbility(User.FindFirst(ClaimTypes.NameIdentifier).Value); ;
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageGetAbilityDTO dto = new PageGetAbilityDTO
            {
                GetAbilityDTO = list,
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateSkills()
        {

            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            CreateAbilityDTO abilityDto = new CreateAbilityDTO();

            PageCreateAbilityDTO dto = new PageCreateAbilityDTO
            {
                GetUserInfoDTO = userDto,
                CreateAbilityDTO = abilityDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSkills([FromBody] PageCreateAbilityDTO baseDto)
        {
            CreateAbilityDTO dto = baseDto.CreateAbilityDTO;

            await _profileSettingService.CreateAbiliy(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction("profile", "profile");
        }

        [Authorize]
        public async Task<IActionResult> DeleteSkills(string skill)
        {
            await _profileSettingService.DeleteUserAbilityAsync(skill);

            return RedirectToAction("skills", "profile");
        }


        //Project
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateProject()
        {
            GetUserInfoDTO userDto = await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageCreateProjectDTO dto = new PageCreateProjectDTO
            {
                GetUserInfoDTO = userDto,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,

            };

            return View(dto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProject(PageCreateProjectDTO baseDto)
        {
            CreateProjectDTO dto = baseDto.CreateProjectDTO;

            await _profileService.CreateProject(dto, User.FindFirst(ClaimTypes.NameIdentifier).Value, _env.WebRootPath);    

            return RedirectToAction("Project", "Profile");
        }

        //Profile
        [HttpGet]
        public async Task<IActionResult> Profile(string? userId)
        {

            string UserId = userId == null ? User.FindFirst(ClaimTypes.NameIdentifier).Value : userId;

            GetUserInfoDTO userDto = User.Identity.IsAuthenticated ? await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value) : new GetUserInfoDTO();
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
            bool? isTakeFriendRequest = User.Identity.IsAuthenticated ? userId != null ? await _friendService.IsTakeFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null : default;
            bool? isSendFriendRequest = User.Identity.IsAuthenticated ? userId != null ? await _friendService.IsSendFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null : default;
            bool? isFriendRequest = User.Identity.IsAuthenticated ? userId != null ? await _friendService.IsFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null : default;
            int friendCount = await _friendService.GetFriendCount(UserId);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


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
                GetNotReadMessageCount = messageCount
            };

            return View(dto);
        }

        [Authorize]
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
            GetUserInfoDTO userDto = User.Identity.IsAuthenticated ? await _authService.GetUserInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value) : new GetUserInfoDTO();
            GetUserProfileDTO profileDto = await _authService.GetUserProfile(UserId);
            int getAllViewer = await _profileService.GetUserViewerCount(UserId);
            int getAllProject = await _profileService.GetUserPostCount(UserId);

            bool? isTakeFriendRequest = User.Identity.IsAuthenticated ? userId != null ? await _friendService.IsTakeFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null : default;
            bool? isSendFriendRequest = User.Identity.IsAuthenticated ? userId != null ? await _friendService.IsSendFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null : default;
            bool? isFriendRequest = User.Identity.IsAuthenticated ? userId != null ? await _friendService.IsFriendRequest(userId, User.FindFirst(ClaimTypes.NameIdentifier).Value) : null : default;
            int friendCount = await _friendService.GetFriendCount(UserId);
            int friendRequestCount = User.Identity.IsAuthenticated ? await _notificationService.GetActiveNotificationCount(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;
            int messageCount = User.Identity.IsAuthenticated ? await _messageService.GetNotReadingMessage(User.FindFirst(ClaimTypes.NameIdentifier).Value) : default;


            PageProfileProjectDTO dto = new PageProfileProjectDTO
            {
                projectsDTOs = projectsDTOs,
                GetUserInfoDTO = userDto,
                GetUserProfileDTO = profileDto,
                GetAllViewer = getAllViewer,
                GetPostCount = getAllProject,
                isTakeFriendRequest = isTakeFriendRequest,
                isSendFriendRequest = isSendFriendRequest,
                isFriendRequest = isFriendRequest,
                GetFriendCount = friendCount / 2,
                GetFriendRequestCount = friendRequestCount,
                GetNotReadMessageCount = messageCount,


            };

            return View(dto);  
        }

    }
}
