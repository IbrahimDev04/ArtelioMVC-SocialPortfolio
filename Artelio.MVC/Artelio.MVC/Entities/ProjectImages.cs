using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class ProjectImages : BaseEntity
    {
        public string ImageUrl { get; set; }

        public string ProjectId {  get; set; }
        public Project Project { get; set; }

        public int Order {  get; set; }
    }
}
