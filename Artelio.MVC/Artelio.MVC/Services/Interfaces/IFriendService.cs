using Artelio.MVC.DTOs.Friend;
using Artelio.MVC.DTOs.Messenger;

namespace Artelio.MVC.Services.Interfaces
{
    public interface IFriendService
    {
        Task SendFriendRequest(string FollowingId, string FollowedId);
        Task<List<GetFriendRequestDTO>> GetAllFriendRequest(string userId);
        Task<int> GetAllFriendRequestCount(string userId);
        Task AcceptFriendRequest(string FollowingId, string FollowedId);
        Task RejectFriendRequest(string FollowingId, string FollowedId);
        Task<bool> IsTakeFriendRequest(string FollowingId, string FollowedId);
        Task<bool> IsSendFriendRequest(string FollowingId, string FollowedId);
        Task<bool> IsFriendRequest(string FollowingId, string FollowedId);
        Task<int> GetFriendCount(string UserId);
        Task<List<GetUserFriendsForMessengerDTO>> GetUserFriends(string UserId);
    }
}
