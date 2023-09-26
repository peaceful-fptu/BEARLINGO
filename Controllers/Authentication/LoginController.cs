using BEARLINGO.Models;
using BEARLINGO.Util;
using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            // redict to Views/Authentication/Login.cshtml
            return View("~/Views/Authentication/Login.cshtml");

        } 
        
        public IActionResult login(string email,string password)
        {
            return View("~/Views/Authentication/Login.cshtml");
        }

    }
}
