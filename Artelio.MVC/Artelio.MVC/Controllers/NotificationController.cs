using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
