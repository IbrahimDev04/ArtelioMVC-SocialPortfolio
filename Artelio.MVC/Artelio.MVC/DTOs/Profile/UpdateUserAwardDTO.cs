namespace Artelio.MVC.DTOs.Profile
{
    public class UpdateUserAwardDTO
    {
        public string Company { get; set; }
        public string AwardName { get; set; }
        public DateTime Date { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
