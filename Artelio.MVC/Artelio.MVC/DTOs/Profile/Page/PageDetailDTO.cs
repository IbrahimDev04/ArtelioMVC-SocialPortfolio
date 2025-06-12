using Artelio.MVC.DTOs.Auth;

namespace Artelio.MVC.DTOs.Profile.Page
{
    public class PageDetailDTO
    {
        public GetProjectOfUserDTO GetProjectDTO { get; set; }
        public GetUserInfoDTO GetUserInfoDTO { get; set; }
        public List<GetAllUserProjectDTO> getAllUserProjectDTOs { get; set; }
    }
}
