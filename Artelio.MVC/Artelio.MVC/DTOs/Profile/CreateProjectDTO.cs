namespace Artelio.MVC.DTOs.Profile
{
    public class CreateProjectDTO
    {
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public IFormFile Image { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
