using System.Threading.Tasks;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class FriendController(IFriendService _friendService) : Controller
    {
        public async Task<IActionResult> SendFriendRequest(string following, string followed)
        {
            await _friendService.SendFriendRequest(following, followed);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RejectFriendRequest(string following, string followed)
        {
            await _friendService.RejectFriendRequest(following, followed);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AcceptFriendRequest(string following, string followed)
        {
            await _friendService.AcceptFriendRequest(following, followed);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UnfriendRequest(string following, string followed)
        {
            await _friendService.RejectFriendRequest(following, followed);
            await _friendService.RejectFriendRequest(followed, following);

            return RedirectToAction("Index", "Home");
        }
    }
}
