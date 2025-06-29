using Artelio.MVC.Entities;

namespace Artelio.MVC.DTOs.Messenger
{
    public class CreateMessageDTO
    {
        public string FromId { get; set; }

        public string ToId { get; set; }

        public string MessageContent { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }
    }
}
