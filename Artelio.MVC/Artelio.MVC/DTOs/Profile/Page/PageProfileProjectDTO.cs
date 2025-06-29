
using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageProfileProjectDTO
    {
        public List<GetUserProjectsDTO> projectsDTOs {  get; set; }
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public GetUserProfileDTO GetUserProfileDTO { get; set; }
        public int GetAllViewer {  get; set; }
        public int GetPostCount { get; set; }


        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }

        public int GetFriendCount { get; set; }
        public bool? isTakeFriendRequest { get; set; }
        public bool? isSendFriendRequest { get; set; }
        public bool? isFriendRequest { get; set; }
    }
}
