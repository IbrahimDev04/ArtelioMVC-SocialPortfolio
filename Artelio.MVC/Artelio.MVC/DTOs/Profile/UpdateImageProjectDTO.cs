namespace Artelio.MVC.DTOs.Profile
{
    public class UpdateImageProjectDTO
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string ProjectId { get; set; }
    }
}
