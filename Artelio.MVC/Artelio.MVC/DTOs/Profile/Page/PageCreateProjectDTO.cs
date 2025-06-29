using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageCreateProjectDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public CreateProjectDTO CreateProjectDTO { get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }


    }
}
