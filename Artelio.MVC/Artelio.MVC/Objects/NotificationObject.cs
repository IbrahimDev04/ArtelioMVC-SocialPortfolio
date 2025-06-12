namespace Artelio.MVC.Objects
{
    public class NotificationObject
    {
        public string ContentId { get; set; }
        public string ContentAction { get; set; }
        public string ContentController { get; set; }
        public string ContentText { get; set; }
        public string? To { get; set; }
    }
}
