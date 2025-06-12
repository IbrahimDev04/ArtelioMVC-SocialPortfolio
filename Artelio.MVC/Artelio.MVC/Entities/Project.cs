using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<UserProject> userProjects { get; set; }
        public ICollection<ProjectImages> projectImages { get; set; }

    }
}
