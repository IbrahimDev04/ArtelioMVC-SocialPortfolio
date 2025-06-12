using Artelio.MVC.DTOs.Profile;
using Artelio.MVC.Entities;

namespace Artelio.MVC.Services.Interfaces
{
    public interface IProfileSettingService
    {
        //Get
        Task<UpdateBasicInfoDTO> GetBasicInfo(string userId);
        Task<UpdateSocialMediaInfoDTO> GetSocialMediaInfo(string userId);
        Task<List<GetUserWorkExperienceDTO>> GetWorkExperiences(string UserId);
        Task<UpdateUserExperienceDTO> GetWorkExperience(string workId);
        Task<List<GetUserAwardDTO>> GetUserAwards(string UserId);
        Task<UpdateUserAwardDTO> GetUserAward(string awardId);
        Task<List<GetLanguageDTO>> GetAllLanguages(string UserId);
        Task<List<GetAbilityDTO>> GetAllAbility(string UserId);
        Task<GetProfileSocialMediaDTO> GetSocialMediaProfile(string UserId);
        Task<List<GetEducationDTO>> GetUserEducations(string UserId);
        Task<UpdateUserEducationDTO> GetUserEducation(string educationId);


        //Create
        Task CreateSocialMediaAsync(string userId);
        Task CreateUserExperienceAsync(CreateUserExperienceDTO dto, string UserId);
        Task CreateUserAward(CreateAwardDTO dto, string UserId, string environment);
        Task CreateUserLanguage(CreateLanguageDTO dto, string UserId, string[] Level);
        Task CreateAbiliy(CreateAbilityDTO dto, string UserId);
        Task CreateEducation(CreateEducationDTO dto, string UserId);


        //Update
        Task UpdateBasicInfo(UpdateBasicInfoDTO data, string userId, string environment);
        Task UpdateSocialMediaInfo(UpdateSocialMediaInfoDTO data, string userId);
        Task UpdateUserExperienceAsync(UpdateUserExperienceDTO data, string workId);
        Task UpdateUserAwardAsync(UpdateUserAwardDTO data, string awardId, string environment);
        Task UpdateUserEducationAsync(UpdateUserEducationDTO data, string educationId);


        //Delete
        Task DeleteUserExperienceAsync(string workId);
        Task DeleteUserAwardAsync(string awardId);
        Task DeleteUserLanguageAsync(string languageId);
        Task DeleteUserAbilityAsync(string abilityId);
        Task DeleteUserEducationAsync(string educationId);


    }
    
}
