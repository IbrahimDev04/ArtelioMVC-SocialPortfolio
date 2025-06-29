using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.DTOs.Friend;

namespace Artelio.MVC.DTOs.Messenger.Page
{
    public class PageGetMessengerDTO
    {
        public List<GetUserFriendsForMessengerDTO> Friends { get; set; }
        public List<GetMessageDTO> Messages { get; set; }
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public GetUserInfoDTO GetRecieveUserDTO { get; set; }
        public string CurrentUserId { get; set; }
        public string RecieveUserId { get; set; }
        public int GetFriendRequestCount { get; set; }
        public int GetNotReadMessageCount { get; set; }

    }
}
