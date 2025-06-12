namespace Artelio.MVC.DTOs.Profile
{
    public class UpdateProjectDTO
    {
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image {  get; set; }
        public List<UpdateImageProjectDTO> ImageUrls { get; set; }
    }
}
