using Artelio.MVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artelio.MVC.DTOs.Profile
{
    public class UpdateUserExperienceDTO
    {
        public string? Company { get; set; }
        public string? JobName { get; set; }
        public CountryEnum? Country { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? About { get; set; }
        public string? WebUrl { get; set; }
    }
}
