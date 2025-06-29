namespace Artelio.MVC.DTOs.Messenger
{
    public class GetMessageDTO
    {
        public string Id { get; set; }
        public string FromId { get; set; }

        public string ToId { get; set; }

        public string MessageContent { get; set; }

        public string Day { get; set; }
        public string Hour { get; set; }

        public bool IsRead { get; set; }
    }
}
