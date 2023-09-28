using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BEARLINGO
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();      
      builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.IOTimeout = TimeSpan.FromSeconds(2);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
      });
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			}).AddCookie().AddGoogle(options =>
			{
				options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
				options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
				options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
				options.CallbackPath = "/dang-nhap-tu-google";
				options.Events.OnTicketReceived = ctx =>
				{
					var userId = ctx.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
					return Task.CompletedTask;
				};
				options.SaveTokens = true;
			}).AddFacebook(options =>
			{
				options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"];
				options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"];
				options.ClaimActions.MapJsonKey("urn:facebook:picture", "picture", "url");
				options.CallbackPath = "/facebookcallback";
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseSession();
			app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}