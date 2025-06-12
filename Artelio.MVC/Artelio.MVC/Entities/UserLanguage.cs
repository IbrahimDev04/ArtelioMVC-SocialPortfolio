using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class UserLanguage : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string LanguageName { get; set; }
        public string ExpertiseLevel { get; set; }
    }
}
