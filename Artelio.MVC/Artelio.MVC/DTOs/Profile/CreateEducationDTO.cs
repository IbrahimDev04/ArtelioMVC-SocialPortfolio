using Artelio.MVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artelio.MVC.DTOs.Profile
{
    public class CreateEducationDTO
    {
        public string SchoolName { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public CountryEnum? Country { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public string City { get; set; }
        public DegreeEnum? Degree { get; set; }
        public IEnumerable<SelectListItem> Degrees { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? About { get; set; }
    }
}
