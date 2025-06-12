using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class SocialMediaLink : BaseEntity
    {
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? GitHubUrl { get; set; }

        public ICollection<UserSocialMedia> userSocialMedias { get; set; }

    }
}
