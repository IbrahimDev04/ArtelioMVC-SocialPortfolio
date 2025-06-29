using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Notification.Page
{
    public class PageNotificationDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }
        public List<GetUserNotificationDTO> notification {  get; set; }


    }
}
