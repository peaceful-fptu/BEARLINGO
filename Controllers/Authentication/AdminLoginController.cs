using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class AdminLoginController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Authentication/AdminLogin.cshtml");
        }
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            if (Username.ToLower().Equals("admin") && Password.ToLower().Equals("admin"))
            {
                HttpContext.Session.SetString("Admin", "true");
                return RedirectToPage("Dashboard");
            }
            return RedirectToAction("Index","AdminLogin");
        }
    }
}
