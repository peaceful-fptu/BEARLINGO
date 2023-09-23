using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
