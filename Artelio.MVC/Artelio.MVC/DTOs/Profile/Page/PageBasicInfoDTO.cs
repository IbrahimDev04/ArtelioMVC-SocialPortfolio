using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageBasicInfoDTO
    {
        public UpdateBasicInfoDTO UpdateBasicInfoDTO { get; set; }
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }

    }
}
