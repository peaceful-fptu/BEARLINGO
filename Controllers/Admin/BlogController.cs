using BEARLINGO.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult addBlog(Blog blog)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                int IDQTV = (int)HttpContext.Session.GetInt32("IDQTV");
                blog.Idqtv = IDQTV;
                ctx.Blogs.Add(blog);
                ctx.SaveChanges();  
            }
            return View("Blog");
        }

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
        public IActionResult editBlog(Blog newBlog)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                Blog oldBlog = ctx.Blogs.FirstOrDefault(bl => bl.Idblog == newBlog.Idblog);
                ctx.Blogs.Remove(oldBlog);
                ctx.Blogs.Add(newBlog);
                ctx.SaveChanges();                  
            }
            return View("Blog");
        }
    }
}
