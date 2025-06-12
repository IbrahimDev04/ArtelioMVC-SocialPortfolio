using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class UserEducation : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string EducationId { get; set; }
        public Education Education { get; set; }
    }
}
