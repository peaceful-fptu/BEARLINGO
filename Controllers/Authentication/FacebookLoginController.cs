using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class FacebookLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
