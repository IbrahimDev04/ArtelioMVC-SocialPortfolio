using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Work : BaseEntity
    {
        public string Company { get; set; }
        public string JobName { get; set; }
        public string Country { get; set; }
        public DateTime StatDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? About { get; set; }  
        public string? WebUrl { get; set; }

        public ICollection<WorkExperience> workExperiences { get; set; }

    }
}
