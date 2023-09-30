using BEARLINGO.Models;
using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Authentication/Register.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(NguoiDung nguoiDung)
        {
            using (var db = new BearlingoContext())
            {
                if (ModelState.IsValid)
                {
                    // check gmail exist
                    var user = db.NguoiDungs.SingleOrDefault(x => x.Gmail == nguoiDung.Gmail);
                    if (user != null)
                    {
                        ViewBag.messageErrorEmail = "Email đã tồn tại!";
                        return View("Index");
                    }
                    // check username exist
                    var user2 = db.NguoiDungs.SingleOrDefault(x => x.TenDangNhap == nguoiDung.TenDangNhap);
                    if (user2 != null)
                    {
                        ViewBag.messageErrorUsername = "Tên đăng nhập đã tồn tại!";
                        return View("Index");
                    }
                    db.NguoiDungs.Add(nguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return RedirectToAction("Index", "Register");
                }
            }
        }

    }
}