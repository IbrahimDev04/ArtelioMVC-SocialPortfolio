namespace Artelio.MVC.DTOs.Notification
{
    public class GetUserNotificationDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ContentId { get; set; }
        public string ContentAction { get; set; }
        public string ContentController { get; set; }
        public string ContentText { get; set; }
        public string Date {  get; set; }
    }
}
