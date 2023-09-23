using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Admin
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
    }
}
