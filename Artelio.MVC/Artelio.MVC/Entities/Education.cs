using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Education : BaseEntity
    {
        public string SchoolName { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Degree { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? About { get; set; }

        public ICollection<UserEducation> userEducations { get; set; }

    }
}
