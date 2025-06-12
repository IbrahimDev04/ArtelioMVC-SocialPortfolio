using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class UserSocialMedia : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string SocialMediaId { get; set; }
        public SocialMediaLink SocialMedia { get; set; }

    }
}
