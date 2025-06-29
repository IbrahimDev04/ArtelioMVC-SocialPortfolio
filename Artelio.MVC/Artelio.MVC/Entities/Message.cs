using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Message : BaseEntity
    {
        public string FromId { get; set; }
        public AppUser From {  get; set; }

        public string ToId { get; set; }
        public AppUser To { get; set; }

        public string MessageContent { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }
    }
}
