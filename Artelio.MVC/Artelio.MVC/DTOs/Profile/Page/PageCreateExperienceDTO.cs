using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageCreateExperienceDTO
    {
        public CreateUserExperienceDTO? CreateUserExperience { get; set; }
        public GetUserInfoDTO? GetUserInfoDTO {  get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }


    }
}
