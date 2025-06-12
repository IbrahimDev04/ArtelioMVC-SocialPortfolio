using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Award : BaseEntity
    {
        public string Company { get; set; }
        public string AwardName { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
    }
}
