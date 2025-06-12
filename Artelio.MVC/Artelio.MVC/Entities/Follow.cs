using Artelio.MVC.Entities.Common;

namespace Artelio.MVC.Entities
{
    public class Follow : BaseEntity
    {
        public string FollowingId { get; set; }
        public AppUser Following { get; set; }

        public string FollowedId { get; set; }
        public AppUser Followed { get; set; }

        public bool? IsAccepted { get; set; }
    }
}
