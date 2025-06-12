using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageGetEducationDTO
    {
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public List<GetEducationDTO> educationDTOs { get; set; }
    }
}
