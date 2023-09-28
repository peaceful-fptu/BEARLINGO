using BEARLINGO.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BEARLINGO.Controllers.Authentication
{
    public class GoogleLoginController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration configuration;
		private readonly BearlingoContext context;

		public GoogleLoginController(ILogger<HomeController> logger, IConfiguration configuration, BearlingoContext context)
		{
			_logger = logger;
			this.configuration = configuration;
			this.context = context;
		}

		public IActionResult OnPost()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Page("/Login", "GoogleCallback"),
			};
			return Challenge(properties, "Google");
		}

		public async Task<IActionResult> GoogleCallback()
		{
			var result = await HttpContext.AuthenticateAsync("Google");

			if (result.Succeeded)
			{
				var accessToken = result.Properties.GetTokenValue("access_token");
				var emailLogin = result?.Principal?.FindFirst(ClaimTypes.Email)?.Value;
				var user = context.NguoiDungs.FirstOrDefault(x => x.Gmail == emailLogin);
				if (user == null)
				{
					user = new NguoiDung();
					user.Gmail = emailLogin;
					user.IDDangNhap = 1; // 1 login google
					context.NguoiDungs.Add(user);
					context.SaveChanges();
				}
				else
				{
					HttpContext.Session.SetString("user", user.IdnguoiDung.ToString());
					// Set session
					// HttpContext.Session.Get("user", user.TenDangNhap);
					// Redirect to home page
					return RedirectToAction("Index", "Home");
				}
			}
			else if (result?.Properties?.Items.ContainsKey("error") == true)
			{
				var error = result.Properties.Items["error"];
				var errorDescription = result.Properties.Items["error_description"];
			}
			return RedirectToPage("Homepage/Home");
		}
	}
}
