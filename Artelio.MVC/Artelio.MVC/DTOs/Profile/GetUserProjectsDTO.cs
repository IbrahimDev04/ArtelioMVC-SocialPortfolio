namespace Artelio.MVC.DTOs.Profile
{
    public class GetUserProjectsDTO
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string? ImageUrl { get; set; }
        public int ViewerCount { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
    }
}
