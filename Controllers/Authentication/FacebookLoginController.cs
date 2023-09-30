using BEARLINGO.Models;
using Facebook;
using Microsoft.AspNetCore.Mvc;

namespace BEARLINGO.Controllers.Authentication
{
    public class FacebookLoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public FacebookLoginController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        [Route("/Login")]
        public ActionResult Index()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = configuration.GetValue<string>("Authentication:Facebook:ClientId"),
                client_secret = configuration.GetValue<string>("Authentication:Facebook:ClientSecret"),
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            ViewBag.Url = loginUrl.AbsoluteUri;

            return View("~/Views/Authentication/Login.cshtml");
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Headers["Referer"].ToString());
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookRedirect");
                return uriBuilder.Uri;
            }
        }
        public ActionResult FacebookRedirect(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("/oauth/access_token", new
            {
                client_id = configuration.GetValue<string>("Authentication:Facebook:ClientId"),
                client_secret = configuration.GetValue<string>("Authentication:Facebook:ClientSecret"),
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code

            });

            fb.AccessToken = result.access_token;

            dynamic me = fb.Get("/me?fields=name,email");
            string email = me.email;
            using (var db = new BearlingoContext())
            {
                var existingUser = db.NguoiDungs.FirstOrDefault(x => x.Gmail == email);
                if (existingUser == null)
                {
                    var user = new NguoiDung();
                    user.Gmail = email;
                    user.IDDangNhap = 2; // 2 login facebook
                    db.NguoiDungs.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    HttpContext.Session.SetString("user", existingUser.IdnguoiDung.ToString());
                    // Set session
                    // HttpContext.Session.Get("user", user.TenDangNhap);
                    // Redirect to home page
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("Success");
        }
    }
}
