using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class UserAbility : BaseEntity
    {
        public string AbilityName { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
