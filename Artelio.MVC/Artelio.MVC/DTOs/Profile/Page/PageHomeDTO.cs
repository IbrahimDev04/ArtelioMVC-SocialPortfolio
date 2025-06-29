using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageHomeDTO
    {
        public List<GetAllProject> getAllProjects {  get; set; }
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public List<GetAllUserProjectDTO> getAllUserProjectDTOs { get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }
    }
}
