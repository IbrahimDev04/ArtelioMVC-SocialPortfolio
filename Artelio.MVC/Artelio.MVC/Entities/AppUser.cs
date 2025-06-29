using Microsoft.AspNetCore.Identity;

namespace Artelio.MVC.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? About { get; set; }
        public string? Job { get; set; }
        public string? Company { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? WebUrl { get; set; }

        public ICollection<UserSocialMedia> userSocialMedias { get; set; }
        public ICollection<WorkExperience> workExperiences { get; set; }
        public ICollection<UserEducation> userEducations { get; set; }
        public ICollection<UserAward> userAwards { get; set; }
        public ICollection<UserAbility> userAbilities { get; set; }
        public ICollection<UserLanguage> userLanguages { get; set; }
        public ICollection<UserProject> userProjects { get; set; }
        public ICollection<Follow> followings { get; set; }
        public ICollection<Follow> followeds { get; set; }
        public ICollection<ViewerProject> viewerProjects { get; set; }
        public ICollection<Notification> notifications { get; set; }
        public ICollection<Message> from { get; set; }
        public ICollection<Message> to { get; set; }




    }
}
