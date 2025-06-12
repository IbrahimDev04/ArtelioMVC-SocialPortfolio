using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class WorkExperience : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string WorkId { get; set; }
        public Work Work { get; set; }
    }
}
