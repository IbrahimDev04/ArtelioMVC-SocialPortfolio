namespace Artelio.MVC.DTOs.Profile
{
    public class GetAllUserProjectDTO
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public List<GetAllImageProjectDTO> ImageUrls { get; set; }
    }
}
