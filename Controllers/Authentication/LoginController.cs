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
		// POST: Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Login(string email, string password)
		{
			using (var db = new BearlingoContext())
			{
				// Find user with email and password
				var user = db.NguoiDungs.SingleOrDefault(x => x.Gmail == email && x.MatKhau == password);
				if (user != null)
				{
					HttpContext.Session.SetString("user", user.IdnguoiDung.ToString());
					// Set session
					// HttpContext.Session.Get("user", user.TenDangNhap);
					// Redirect to home page
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Redirect to login page
					return RedirectToAction("Index", "Login");
				}
			}
		}

	}
}
