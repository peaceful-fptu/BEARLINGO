using BEARLINGO.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BEARLINGO
{
    public class Program
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddDbContext<BearlingoContext>(o => o.UseSqlServer("MyBearlingo"));
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.IOTimeout = TimeSpan.FromSeconds(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(builder.Environment.ContentRootPath)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = configuration["Jwt:Issuer"],
                                ValidAudience = configuration["Jwt:Issuer"],
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                            };
                    });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Roles.Admin, policy => policy.RequireRole(Roles.Admin));
                options.AddPolicy(Roles.User, policy => policy.RequireRole(Roles.User));
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
                options.CallbackPath = "/signin-facebook";
                options.Events.OnTicketReceived = ctx =>
                {
                    var userId = ctx.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    return Task.CompletedTask;
                };
                options.SaveTokens = true;
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}