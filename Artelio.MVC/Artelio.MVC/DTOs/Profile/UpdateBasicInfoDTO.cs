using Artelio.MVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artelio.MVC.DTOs.Profile
{
    public class UpdateBasicInfoDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public string Company { get; set; }
        public CountryEnum Country { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public string City { get; set; }
        public string WebUrl { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }    
        public IFormFile Image { get; set; } 
    }
}
