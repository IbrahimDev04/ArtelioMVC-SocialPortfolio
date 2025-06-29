using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Messenger;
using Artelio.MVC.Entities;
using Artelio.MVC.Services.Interfaces;
using Azure.Messaging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Artelio.MVC.Hubs
{
    public class ChatHub(IMessageService _messageService, ArtelioContext _context) : Hub
    {
        private static ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, bool> OnlineUsers = new();

        private static Dictionary<string, string> ActiveChats = new(); // userId => talkingWithUserId

        public async Task SetCurrentChat(string userId, string chattingWithId)
        {
            ActiveChats[userId] = chattingWithId;

            // Burada avtomatik unread mesajları "görülmüş" et
            var unreadMessages = await _context.messages
                .Where(m => m.FromId == chattingWithId && m.ToId == userId && !m.IsRead)
                .ToListAsync();

            foreach (var msg in unreadMessages)
            {
                msg.IsRead = true;

                // İstifadəçiyə messageStatus göndər
                await Clients.User(userId).SendAsync("messageStatus", msg.Id, true);
            }

            await _context.SaveChangesAsync();
        }

        
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                UserConnections[Context.ConnectionId] = userId;
                OnlineUsers[userId] = true;

                await NotifyFriendsOnlineAsync(userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (UserConnections.TryRemove(Context.ConnectionId, out var userId))
            {
                OnlineUsers.TryRemove(userId, out _);

                var userFriends = await _context.Follows
                    .Where(f => f.FollowingId == userId && f.IsAccepted == true)
                    .Select(f => f.FollowedId)
                    .ToListAsync();

                foreach (var friendId in userFriends)
                {
                    await Clients.User(friendId).SendAsync("friendOffline", userId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<string> SendMessageAsync(string message, string recieveUserId, string currentUserId, DateTime date)
        {
            date = date.AddHours(4);

            // İstifadəçinin chatda olub-olmamasına görə IsRead təyin edilir
            bool isRead = ActiveChats.TryGetValue(currentUserId, out string activeChatUser) && activeChatUser == recieveUserId;

            CreateMessageDTO messageContent = new CreateMessageDTO
            {
                Date = date,
                FromId = recieveUserId,
                ToId = currentUserId,
                MessageContent = message,
                IsRead = isRead
            };

            // Mesajı bazaya yazırıq və ID-ni alırıq
            string messageId = await _messageService.CreateMessage(messageContent);

            string hour = date.ToString("HH:mm");
            string day = date.Day == DateTime.Now.Day ? "Today" : date.Day + " " + date.ToString("MMMM");

            // Qarşı tərəfə mesajı göndəririk
            await Clients.User(currentUserId).SendAsync("recieveMessage", message, currentUserId, recieveUserId, hour, day, messageId);

            // Göndərənə mesajın oxunma statusunu bildiririk
            await Clients.User(recieveUserId).SendAsync("messageStatus", messageId, isRead);

            if (isRead)
            {
                // Əgər istifadəçi chatdadırsa, görüldü göndər
                await Clients.User(recieveUserId).SendAsync("messageStatus", messageId, isRead);
            }
            else
            {
                // Chatda deyilsə, alert göndər
                await Clients.User(currentUserId).SendAsync("unreadMessageAlert", messageId, recieveUserId);
            }

            return messageId;
        }

        public async Task TypingAsync(string fromUserId, string toUserId)
        {
            if (fromUserId != Context.UserIdentifier)
                return;

            await Clients.User(toUserId).SendAsync("showTyping", fromUserId, toUserId);
        }

        public async Task NotifyFriendsOnlineAsync(string userId)
        {
            var userFriends = await _context.Follows
                .Where(f => f.FollowingId == userId && f.IsAccepted == true)
                .Select(f => f.FollowedId)
                .ToListAsync();

            foreach (var friendId in userFriends)
            {
                await Clients.User(friendId).SendAsync("friendOnline", userId);
            }
        }

        public Task<List<string>> GetOnlineFriends(string userId)
        {
            var onlineFriends = _context.Follows
                .Where(f => f.FollowingId == userId && f.IsAccepted == true)
                .Select(f => f.FollowedId)
                .ToList()
                .Where(friendId => OnlineUsers.ContainsKey(friendId))
                .ToList();

            return Task.FromResult(onlineFriends);
        }

        public async Task MarkAsRead(string fromId, string toId)
        {
            var unreadMessages = await _context.messages
                .Where(m => m.FromId == fromId && m.ToId == toId && !m.IsRead)
                .ToListAsync();

            foreach (var msg in unreadMessages)
            {
                msg.IsRead = true;

                // Frontend-ə mesaj statusunu dəyiş
                await Clients.User(toId).SendAsync("messageStatus", msg.Id, true); // ✅ status green olacaq
            }

            await _context.SaveChangesAsync();
        }


    }
}
