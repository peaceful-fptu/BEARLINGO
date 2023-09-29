using BEARLINGO.Models;
using Facebook;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BEARLINGO.Controllers.Authentication
{
    public class FacebookLoginController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration configuration;
        private readonly BearlingoContext context;

        public FacebookLoginController(ILogger<HomeController> logger, IConfiguration configuration, BearlingoContext context)
        {
            _logger = logger;
            this.configuration = configuration;
            this.context = context;
        }

        public ActionResult Index()
		{
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FacebookRedirect", "FacebookLogin"),
            };
            return Challenge(authenticationProperties, "Facebook");
        }

		public async Task<ActionResult> FacebookRedirect()
		{
            var result = await HttpContext.AuthenticateAsync("Facebook");

            if (result.Succeeded)
            {
                var emailLogin = result?.Principal?.FindFirst(ClaimTypes.Email)?.Value;
                var name = result?.Principal?.FindFirst(ClaimTypes.Name)?.Value;
                NguoiDung nguoiDung = context.NguoiDungs.Where(n => n.Gmail == emailLogin).SingleOrDefault();
                if (nguoiDung == null)
                {
                    ViewBag.report = "Bạn cần đăng kí tài khoản trước khi sử dụng dịch vụ";
                    ViewBag.email = emailLogin;
                    ViewBag.name = name;
                    return View("~/Views/Authentication/Register.cshtml");
                }
                else
                {
                    HttpContext.Session.SetInt32("user", nguoiDung.IdnguoiDung);
                    // Set session
                    // HttpContext.Session.Get("user", user.TenDangNhap);
                    // Redirect to home page
                    return RedirectToAction("Index", "Home");
                }              
            }
            return RedirectToAction("Index");
        }
	}
}
