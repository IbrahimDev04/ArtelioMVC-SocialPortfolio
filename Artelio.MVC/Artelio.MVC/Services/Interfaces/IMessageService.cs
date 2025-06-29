using Artelio.MVC.DTOs.Messenger;
using Artelio.MVC.Entities;

namespace Artelio.MVC.Services.Interfaces
{
    public interface IMessageService
    {
        Task<string> CreateMessage(CreateMessageDTO dto);
        Task<List<GetMessageDTO>> GetCurrentUserMessage(string FromId, string ToId);
        Task MarkAsRead(string fromId, string toId);
        Task<int> GetNotReadingMessage(string userId);
    }
}
    