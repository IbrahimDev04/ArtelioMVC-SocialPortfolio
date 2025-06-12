using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class UserProject : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<ViewerProject> viewerProjects { get; set; }

    }
}
