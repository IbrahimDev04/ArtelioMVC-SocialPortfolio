namespace Artelio.MVC.DTOs.Profile
{
    public class GetEducationDTO
    {
        public string Id { get; set; }
        public string SchoolName { get; set; }
        public string Faculty { get; set;}
        public string Specialty { get; set;}
        public string Country { get; set; }
        public string City { get; set; }
        public string Degree { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string? About { get; set; }
    }
}
