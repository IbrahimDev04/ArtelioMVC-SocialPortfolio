using System.Globalization;
using Artelio.MVC.Contexts;
using Artelio.MVC.DTOs.Messenger;
using Artelio.MVC.Entities;
using Artelio.MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Artelio.MVC.Services.Implements
{
    public class MessageService(ArtelioContext _context) : IMessageService
    {
        public async Task<string> CreateMessage(CreateMessageDTO dto)
        {
            Message message = new Message
            {
                Date = DateTime.Now,
                FromId = dto.FromId,
                ToId = dto.ToId,
                MessageContent = dto.MessageContent,
                IsRead = dto.IsRead,
            };

            await _context.messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message.Id;
        }

        public async Task<List<GetMessageDTO>> GetCurrentUserMessage(string FromId, string ToId)
        {
            List<GetMessageDTO> dto = await _context.messages.Where(u => (u.FromId == FromId && u.ToId == ToId) || (u.FromId == ToId && u.ToId == FromId)).OrderBy(u => u.Date).Select(u => new GetMessageDTO
            {
                Id = u.Id,
                FromId = u.FromId,
                ToId = u.ToId,
                Day = u.Date.Day == DateTime.Now.Day ? "Today" : u.Date.Day.ToString() + " " + u.Date.ToString("MMMM", CultureInfo.InvariantCulture),
                Hour = u.Date.ToString("HH:mm"),
                MessageContent = u.MessageContent,
                IsRead = u.IsRead,

            }).ToListAsync();

            return dto;
        }

        public async Task<int> GetNotReadingMessage(string userId)
        {
            return await _context.messages.CountAsync(u => u.ToId == userId && u.IsRead == false);
        }

        public async Task MarkAsRead(string fromId, string toId)
        {
            var unreadMessages = await _context.messages
                .Where(m => m.FromId == fromId && m.ToId == toId && !m.IsRead)
                .ToListAsync();

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }

    }
}
