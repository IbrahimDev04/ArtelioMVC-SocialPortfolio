using Microsoft.AspNetCore.Mvc;

namespace Artelio.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public async Task<IActionResult> ErrorPage()
        {
            return View();
        }
    }
}
