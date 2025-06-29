using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Friend;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageProfileDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public GetUserProfileDTO GetUserProfileDTO { get; set; }
        public GetProfileUserAboutDTO GetProfileUserAboutDTO { get; set; }
        public GetProfileSocialMediaDTO GetProfileSocialMediaDTO { get; set; }
        public List<GetUserWorkExperienceDTO> GetUserWorkExperienceDTOs { get; set; }
        public List<GetUserAwardDTO> GetUserAwardDTO { get; set; }
        public List<GetAbilityDTO> GetAbilityDTO { get; set; }
        public List<GetLanguageDTO> Languages { get; set; }
        public List<GetEducationDTO> getUserEducationDTOs { get; set; }


        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }

        public int GetAllViewer { get; set; }
        public int GetPostCount { get; set; }
        public int GetFriendCount { get; set; }
        public bool? isTakeFriendRequest { get; set; }
        public bool? isSendFriendRequest { get; set; }
        public bool? isFriendRequest { get; set; }

    }
}
