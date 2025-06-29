using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string? To { get; set; }

        public string ContentId { get; set; }

        public string ContentAction { get; set; }
        public string ContentController {  get; set; }

        public string ContentText { get; set; }

        public DateTime Date { get; set; }

        public bool isActive { get; set; }
    }
}
