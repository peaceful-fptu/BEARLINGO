using BEARLINGO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BEARLINGO.Program;

namespace BEARLINGO.Controllers.Admin
{
    public class BlogController : Controller
    {
        public IActionResult BlogManage()
        {
            using(BearlingoContext ctx = new BearlingoContext())
            {
                var blogs = ctx.Blogs.ToList();
                ViewBag.blogs = blogs;
            }
            return View();
        }
        [Authorize(Policy = Roles.Admin)]
        public IActionResult addBlog() { return View(); }   

        [HttpPost]
        public IActionResult addBlog(Blog blog)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                Blog checkBlog = ctx.Blogs.FirstOrDefault(bl => bl.Idblog == blog.Idblog);
                if(checkBlog == null) {
                    int IDQTV = (int)HttpContext.Session.GetInt32("IDQTV");
                    blog.Idqtv = IDQTV;
                    ctx.Blogs.Add(blog);
                    ctx.SaveChanges();
                    return RedirectToAction("BlogManage");
                }else
                {
                    String errorMessage = "Blog is already existed!";
                    ViewBag.message = errorMessage;
                    return View();
                }            
            }
      
        }
        [Authorize(Policy = Roles.Admin)]
        public IActionResult deleteBlog(int id)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                Blog blog = ctx.Blogs.FirstOrDefault(bl => bl.Idblog ==  id);
                ctx.Blogs.Remove(blog);
                ctx.SaveChanges();
            }
            return View("Blog");
        }

        [HttpPost]
        [Authorize(Policy = Roles.Admin)]
        public IActionResult editBlog(Blog newBlog)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                ctx.Blogs.Update(newBlog);
                ctx.SaveChanges();                  
            }
            return View("Blog");
        }
    }
}
