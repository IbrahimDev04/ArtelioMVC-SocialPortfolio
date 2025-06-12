using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class UserAward : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string AwardId { get; set; }
        public Award Award { get; set; }
    }
}
