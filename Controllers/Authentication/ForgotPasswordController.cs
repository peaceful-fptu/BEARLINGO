using BEARLINGO.Models;
using BEARLINGO.Util;
using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                NguoiDung user = ctx.NguoiDungs.FirstOrDefault(u => u.Gmail.Equals(email));
                if (user == null)
                {
                    string messageError = "Không tìm thấy tài khoản trùng khớp với email được cung cấp";
                    return View("ForgotPassword", messageError);
                }
                else
                {
                    Random rd = new Random();
                    int otpCode = rd.Next(100000, 1000000);
                    string body = "Your reset code is: " + otpCode + "\n Don't let anyone know this code!";
                    MailSender.SendMail(email, "Reset code", body);
                    ViewBag.Email = email;
                    return View("ConfirmOtp", otpCode.ToString());
                }
            }
        }

        public IActionResult ConfirmOtp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmOtp(string userCode, string systemCode, string email)
        {
            ViewBag.Email = email;
            if(userCode.Equals(systemCode))
            {
                return View("ResetPassword");
            }else
            {
                string messageError = "Mã otp bạn nhập không trùng khớp với mã otp mà hệ thống đã gửi!";
                return View("ConfirmOtp", messageError);
            }
        }

        public IActionResult ResetPassword() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string newPass, string email)
        {
            using(BearlingoContext ctx = new BearlingoContext())
            {
                NguoiDung user = ctx.NguoiDungs.FirstOrDefault(u => u.Gmail.Equals(email));
                ctx.NguoiDungs.Remove(user);
                user.MatKhau = newPass;
                ctx.NguoiDungs.Add(user);
                ctx.SaveChanges();
                return RedirectToAction("Login");
            }
        }

    }
}
