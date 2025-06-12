namespace Artelio.MVC.DTOs.Auth
{
    public class GetUserProfileDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string ImageUrl { get; set; }
        public string BannerUrl { get; set; }
        public IFormFile Banner { get; set; }

    }
}
