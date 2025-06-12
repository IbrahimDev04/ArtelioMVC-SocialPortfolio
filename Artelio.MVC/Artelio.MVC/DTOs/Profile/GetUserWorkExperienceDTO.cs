namespace Artelio.MVC.DTOs.Profile
{
    public class GetUserWorkExperienceDTO
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string JobName { get; set; }
        public string Country { get; set; }
        public string WebUrl { get; set; }
        public string StatDate { get; set; }
        public string? EndDate { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? About { get; set; }
        public string? UserId { get; set; }
    }
}
