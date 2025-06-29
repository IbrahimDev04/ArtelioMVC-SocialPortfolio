namespace Artelio.MVC.DTOs.Messenger
{
    public class GetUserFriendsForMessengerDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string LastMessageContent {  get; set; }
    }
}
