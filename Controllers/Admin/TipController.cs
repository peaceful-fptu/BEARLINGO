using BEARLINGO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BEARLINGO.Program;

namespace BEARLINGO.Controllers.Admin
{
    public class TipController : Controller
    {
        public IActionResult tipManage()
        {
            using(BearlingoContext ctx = new BearlingoContext())
            {
                var tips = ctx.Tips.ToList();
                ViewBag.tips = tips;
            }
            return View();
        }

        [HttpPost]
        [Authorize(Policy = Roles.Admin)]
        public IActionResult addTip(Tip tip)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                ctx.Tips.Add(tip);
                ctx.SaveChanges();
            }
            return RedirectToAction("tipManage");
        }

        [HttpPost]
        [Authorize(Policy = Roles.Admin)]
        public IActionResult editTip(Tip newTip)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                Tip oldTip = ctx.Tips.FirstOrDefault(t => t.Idtips == newTip.Idtips);
                ctx.Tips.Remove(oldTip);
                ctx.Tips.Add(newTip);
                ctx.SaveChanges();
            }
            return View();
        }
        [Authorize(Policy = Roles.Admin)]
        public IActionResult deleteTip(int id)
        {
            using (BearlingoContext ctx = new BearlingoContext())
            {
                Tip tip = ctx.Tips.FirstOrDefault(t => t.Idtips == id);
                ctx.Tips.Remove(tip);
                ctx.SaveChanges();  
            }
            return RedirectToAction("tipManage");
        }
    }
}
