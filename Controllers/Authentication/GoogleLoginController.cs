using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class GoogleLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
