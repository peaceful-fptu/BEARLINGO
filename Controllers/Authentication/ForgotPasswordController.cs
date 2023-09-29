using BEARLINGO.Models;
using BEARLINGO.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEARLINGO.Controllers.Authentication
{
    public class ForgotPasswordController : Controller
    {
        BearlingoContext ctx = new BearlingoContext();

        public IActionResult ForgotPassword()
        {
            return View("~/Views/Authentication/Forgetpass.cshtml");
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                var user = ctx.NguoiDungs.FirstOrDefault(u => u.Gmail.Equals(email));
                if (user == null)
                {
                    string messageError = "Không tìm thấy tài khoản trùng khớp với email được cung cấp";
                    ViewBag.messageError = messageError;
                    return ConfirmOtp();
                }
                else
                {
                    Random rd = new Random();
                    int otpCode = rd.Next(100000, 1000000);
                    HttpContext.Session.SetSession("otpCode", otpCode.ToString());
                    HttpContext.Session.SetSession("email", email);
                    string body = "Your reset code is: " + otpCode + "\n Don't let anyone know this code!";
                    MailSender.SendMail(email, "Reset code", body);
                    ViewBag.Email = email;
                    return View("~/Views/Authentication/ConfirmOtp.cshtml");
                }
            }
        }

        public IActionResult ConfirmOtp()
        {
            return View("~/Views/Authentication/ConfirmOtp.cshtml");
        }

        [HttpPost]
        public IActionResult ConfirmOtp(string userCode)
        {
            string otpCode = HttpContext.Session.GetSession<string>("otpCode");
            if (userCode.Equals(otpCode))
            {
                return RedirectToAction("ChangePassword");
            }
            else
            {
                string messageError = "Mã otp bạn nhập không trùng khớp với mã otp mà hệ thống đã gửi!";
                ViewBag.messageError = messageError;
                return View("ConfirmOtp");
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string newPass)
        {
            string email = HttpContext.Session.GetSession<string>("email");
            NguoiDung user = ctx.NguoiDungs.FirstOrDefault(u => u.Gmail.Equals(email));
            user.MatKhau = newPass;
            ctx.NguoiDungs.Update(user);
            ctx.SaveChanges();
            return View("~/Views/Authentication/Login.cshtml");
        }

    }
}
