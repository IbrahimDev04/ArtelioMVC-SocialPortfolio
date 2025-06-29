using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageSocialMediaInfoDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public UpdateSocialMediaInfoDTO UpdateSocialMediaInfoDTO { get; set; }
        public int GetFriendRequestCount { get; set; }

        public int GetNotReadMessageCount { get; set; }

    }
}
