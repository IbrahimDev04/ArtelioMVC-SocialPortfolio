using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Friend;
using Artelio.MVC.DTOs.Messenger;
using Artelio.MVC.Entities;
using Artelio.MVC.Objects;
using Artelio.MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artelio.MVC.Services.Implements
{
    public class FriendService(ArtelioContext _context, INotificationService _notificationService) : IFriendService
    {
        public async Task SendFriendRequest(string FollowingId, string FollowedId)
        {
            Follow follow = new Follow
            {
                IsAccepted = false,
                FollowingId = FollowingId,
                FollowedId = FollowedId
            }; 



            await _context.Follows.AddAsync(follow);

            NotificationObject @object = new NotificationObject
            {
                ContentId = FollowingId,
                To = FollowedId,
                ContentAction = "Profile",
                ContentController = "Profile",
                ContentText = "sent you a friend request. You can answer to request."
            };

            await _context.SaveChangesAsync();

            await _notificationService.CreateNotification(FollowingId, @object);
        }

        public async Task<List<GetFriendRequestDTO>> GetAllFriendRequest(string userId)
        {
            List<GetFriendRequestDTO> dto = await _context.Follows.Where(u => u.IsAccepted == false && u.FollowedId == userId).Select(u => new GetFriendRequestDTO
            {
                Id = u.FollowingId,
                UserName = u.Following.UserName,
                ImageUrl = u.Following.ImageUrl
            }).ToListAsync();

        return dto;
        }

        public async Task AcceptFriendRequest(string FollowingId, string FollowedId)
        {
            Follow follow = new Follow
            {
                FollowingId = FollowingId,
                FollowedId = FollowedId,
                IsAccepted = true
            };

            await _context.Follows.AddAsync(follow);

            Follow follower = await _context.Follows.FirstOrDefaultAsync(u => u.FollowingId == FollowedId && u.FollowedId == FollowingId);

            follower.IsAccepted = true;

            NotificationObject @object = new NotificationObject
            {
                ContentId = FollowingId,
                To = FollowedId,
                ContentAction = "Profile",
                ContentController = "Profile",
                ContentText = " user is accept your friend request."
            };

            await _context.SaveChangesAsync();

            await _notificationService.CreateNotification(FollowingId, @object);
        }

        public async Task RejectFriendRequest(string FollowingId, string FollowedId)
        {
            Follow follow = await _context.Follows.FirstOrDefaultAsync(u => u.FollowingId == FollowingId);

            _context.Follows.Remove(follow);

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetFriendCount(string UserId)
        {
             return await _context.Follows.CountAsync(u => (u.FollowingId == UserId && u.IsAccepted == true) || (u.FollowedId == UserId && u.IsAccepted == true));
        }

        public async Task<bool> IsTakeFriendRequest(string FollowingId, string FollowedId)
        {
            return await _context.Follows.AnyAsync(u => u.FollowedId == FollowedId && u.IsAccepted == false && u.FollowingId == FollowingId);
        }

        public async Task<bool> IsSendFriendRequest(string FollowingId, string FollowedId)
        {
            return await _context.Follows.AnyAsync(u => u.FollowedId == FollowingId && u.FollowingId == FollowedId && u.IsAccepted == false);
        }

        public async Task<bool> IsFriendRequest(string FollowingId, string FollowedId)
        {
            return await _context.Follows.AnyAsync(u => (u.FollowedId == FollowedId && u.IsAccepted == true && u.FollowingId == FollowingId) || (u.FollowedId == FollowingId && u.IsAccepted == true && u.FollowingId == FollowedId) );
        }

        public async Task<int> GetAllFriendRequestCount(string userId)
        {
            return await _context.Follows.CountAsync(u => u.IsAccepted == false && u.FollowedId == userId);
        }

        public async Task<List<GetUserFriendsForMessengerDTO>> GetUserFriends(string UserId)
        {
            var friends = await _context.Follows
                .Where(u => u.FollowingId == UserId && u.IsAccepted == true)
                .Select(u => new
                {
                    u.FollowedId,
                    u.Followed.UserName,
                    u.Followed.ImageUrl
                })
                .ToListAsync();

            var dtoList = new List<GetUserFriendsForMessengerDTO>();

            foreach (var friend in friends)
            {
                var messages = await _context.messages
                    .Where(m =>
                        (m.FromId == UserId && m.ToId == friend.FollowedId) ||
                        (m.FromId == friend.FollowedId && m.ToId == UserId))
                    .OrderByDescending(m => m.Date)
                    .Take(2)
                    .Reverse()
                    .ToListAsync();

                string lastMessage = messages.Count > 1 ? messages[1].MessageContent :
                                     messages.Count == 1 ? messages[0].MessageContent :
                                     null;

                dtoList.Add(new GetUserFriendsForMessengerDTO
                {
                    UserId = friend.FollowedId,
                    UserName = friend.UserName,
                    ImageUrl = friend.ImageUrl,
                    LastMessageContent = lastMessage
                });
            }

            return dtoList;

        }
    }
}
