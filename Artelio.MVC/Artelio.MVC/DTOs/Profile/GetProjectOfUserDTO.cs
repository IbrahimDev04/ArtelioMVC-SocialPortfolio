namespace Artelio.MVC.DTOs.Profile
{
    public class GetProjectOfUserDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public List<GetImageProjectDTO> ImageUrls { get; set; }
    }
}
