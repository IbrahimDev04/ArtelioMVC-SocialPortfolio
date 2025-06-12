using Artelio.MVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Artelio.MVC.DTOs.Profile
{
    public class CreateLanguageDTO
    {
        public LanguageEnum? Language { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        public int ExpertiseLevel { get; set; }
    }
}
