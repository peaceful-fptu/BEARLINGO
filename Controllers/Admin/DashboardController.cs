using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BEARLINGO.Program;

namespace BEARLINGO.Controllers.Admin
{
    public class DashboardController : Controller
    {
        [Authorize(Policy = Roles.Admin)]
        public IActionResult Index()
        {
            return View("~/Views/AdminPage/Dashboard.cshtml");
        }
    }
}
