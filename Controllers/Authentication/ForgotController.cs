using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class ForgotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
