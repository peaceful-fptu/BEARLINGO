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

        [HttpGet]
        public IActionResult ConfirmOtp()
        {
            return View("~/Views/Authentication/ConfirmOtp.cshtml");
        }

        [HttpPost]
        public IActionResult ConfirmOtp(string number1, string number2, string number3, string number4, string number5, string number6)
        {
            string userCode = number1 + number2 + number3 + number4 + number5 + number6;
            string otpCode = HttpContext.Session.GetSession<string>("otpCode");
            if (userCode.Equals(otpCode))
            {
                return RedirectToAction("ChangePassword");
            }
            else
            {
                string messageError = "Mã otp bạn nhập không trùng khớp với mã otp mà hệ thống đã gửi!";
                ViewBag.messageError = messageError;
                return View("~/Views/Authentication/ConfirmOtp.cshtml");
            }
        }

        public IActionResult ChangePassword()
        {
            return View("~/Views/Authentication/ChangePassword.cshtml");
        }

        [HttpPost]
        public IActionResult ChangePassword(string newPass)
        {
            string email = HttpContext.Session.GetSession<string>("email");
            NguoiDung user = ctx.NguoiDungs.FirstOrDefault(u => u.Gmail.Equals(email));
            user.MatKhau = newPass;
            ctx.NguoiDungs.Update(user);
            ctx.SaveChanges();
            MailSender.SendMail(email, "Đổi mật khẩu thành công", "Bạn vừa đổi mật khẩu thành công.Từ lần đăng nhập sau bạn có thể đăng nhập bằng mật khẩu mới này");
            return View("~/Views/Authentication/Login.cshtml");
        }

    }
}
