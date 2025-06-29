using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageCreateEducationDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public CreateEducationDTO CreateEducationDTO { get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }

    }
}
