using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class ViewerProject : BaseEntity
    {
        public string ProjectId {  get; set; }
        public UserProject Project { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }


    }
}
