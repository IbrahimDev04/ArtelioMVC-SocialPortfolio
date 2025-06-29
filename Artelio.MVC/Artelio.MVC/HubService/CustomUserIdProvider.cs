using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Artelio.MVC.HubService
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Use NameIdentifier from Claims as user id
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
