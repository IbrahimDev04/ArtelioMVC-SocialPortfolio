using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageGetAwardDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public List<GetUserAwardDTO> GetUserAwardDTO {  get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }
            
    }
}
