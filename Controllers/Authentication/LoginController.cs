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
                // check email exist in database
                var user1 = db.NguoiDungs.SingleOrDefault(x => x.Gmail == email);
                if (user1 == null)
                {
                    ViewBag.messageErrorEmail = "Email không tồn tại!";
                    return View("Index");
                }
                else if (user1.MatKhau != password)
                {
                    ViewBag.messageErrorPassword = "Mật khẩu không đúng!";
                    return View("Index");
                }
                var user = db.NguoiDungs.SingleOrDefault(x => x.Gmail == email && x.MatKhau == password);
                if (user != null)
                {
                    HttpContext.Session.SetString("User", user.IdnguoiDung.ToString());
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
