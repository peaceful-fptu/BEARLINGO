using BEARLINGO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace BEARLINGO.Controllers.Admin
{
    public class GrammarController : Controller
    {
        private readonly BearlingoContext _context;

        public GrammarController()
        {
            _context = new BearlingoContext();
        }

        public IActionResult Topic()
        {
            var listChuDe = this.getChuDe();
            ViewData["listChuDe"] = listChuDe;
            return View();
        }

        [HttpGet]
        public IActionResult TopicDetail(int idNguPhap)
        {
            var listNguPhap = this.getNguPhap(idNguPhap);
            ViewData["listNguPhap"] = listNguPhap;
            return View();
        }

        public List<ChuDeNguPhap> getChuDe()
        {
            var result = from chude in this._context.ChuDeNguPhaps
                         join qtv in this._context.Qtvs
                            on chude.Idqtv equals qtv.Idqtv
                         select new ChuDeNguPhap
                         {
                             IdchuDeNguPhap = chude.IdchuDeNguPhap,
                             TenNguPhap = chude.TenNguPhap,
                             Stt = chude.Stt,
                             Idqtv = qtv.Idqtv,
                         };

            if (result != null)
            {
                return result.ToList();
            }

            return new List<ChuDeNguPhap>();
        }

        public List<NguPhap> getNguPhap(int id)
        {
            var result = from nguphap in this._context.NguPhaps
                         join chude in this._context.ChuDeNguPhaps
                            on nguphap.IdchuDeNguPhap equals chude.IdchuDeNguPhap
                         where nguphap.IdchuDeNguPhap == id
                         select new NguPhap
                         {
                             IdnguPhap = nguphap.IdnguPhap,
                             TieuDe = nguphap.TieuDe,
                             CachDung = nguphap.CachDung,
                             CauTruc = nguphap.CauTruc,
                             ViDu = nguphap.ViDu,
                             BoSung = nguphap.BoSung,
                             LuuY = nguphap.LuuY,
                             IdchuDeNguPhap = chude.IdchuDeNguPhap,
                         };

            if (result != null)
            {
                return result.ToList();
            }

            return new List<NguPhap>();
        }

        [HttpPost]
        public IActionResult AddChuDe(string tenNguPhap, int stt, int idQtv)
        {
            try
            {
                ChuDeNguPhap chuDeNguPhap = new ChuDeNguPhap
                {
                    TenNguPhap = tenNguPhap,
                    Stt = stt,
                    Idqtv = idQtv,
                };

                this._context.ChuDeNguPhaps.Add(chuDeNguPhap);
                this._context.SaveChanges();
                return RedirectToAction("Topic", "Grammar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddNguPhap(string tieuDe, string cachDung, string cauTruc, string viDu, string boSung, string luuY, int idChuDe)
        {
            try
            {
                NguPhap nguPhap = new NguPhap
                {
                    TieuDe = tieuDe,
                    CachDung = cachDung,
                    CauTruc = cauTruc,
                    ViDu = viDu,
                    BoSung = boSung,
                    LuuY = luuY,
                    IdchuDeNguPhap = idChuDe,
                };

                this._context.NguPhaps.Add(nguPhap);
                this._context.SaveChanges();

                return RedirectToAction("TopicDetail", "Grammar", new { idNguPhap = idChuDe });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteChuDe(int idChuDe)
        {
            var chuDe = this._context.ChuDeNguPhaps.FirstOrDefault(item => item.IdchuDeNguPhap == idChuDe);
            var nguPhap = this._context.NguPhaps.Where(item => item.IdchuDeNguPhap == idChuDe).ToList();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (nguPhap != null)
                    {
                        this._context.NguPhaps.RemoveRange(nguPhap);
                        this._context.SaveChanges();
                    }
                    if (chuDe != null)
                    {
                        this._context.ChuDeNguPhaps.Remove(chuDe);
                        this._context.SaveChanges();
                    }

                    transaction.Commit();

                    return RedirectToAction("Topic", "Grammar");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return View();
            }
        }

        [HttpGet]
        public IActionResult DeleteNguPhap(int idNguPhap)
        {
            var nguPhap = this._context.NguPhaps.FirstOrDefault(item => item.IdnguPhap == idNguPhap);
            try
            {
                if (nguPhap != null)
                {
                    this._context.Remove(nguPhap);
                    this._context.SaveChanges();
                }

                return RedirectToAction("TopicDetail", "Grammar", new { idNguPhap = idNguPhap });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateChuDe(int idChuDe, string tenNguPhap, int stt, int idQtv)
        {
            var chuDe = this._context.ChuDeNguPhaps.FirstOrDefault(item => item.IdchuDeNguPhap == idChuDe);
            try
            {
                if (chuDe != null)
                {
                    chuDe.TenNguPhap = tenNguPhap;
                    chuDe.Stt = stt;
                    chuDe.Idqtv = idQtv;
                }

                return RedirectToAction("Topic", "Grammar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateNguPhap(int idNguPhap, string tieuDe, string cachDung, string cauTruc, string viDu, string boSung, string luuY, int idChuDe)
        {
            var nguPhap = this._context.NguPhaps.FirstOrDefault(item => item.IdnguPhap == idNguPhap);
            try
            {
                if (nguPhap != null)
                {
                    nguPhap.TieuDe = tieuDe;
                    nguPhap.CachDung = cachDung;
                    nguPhap.CauTruc = cauTruc;
                    nguPhap.ViDu = viDu;
                    nguPhap.BoSung = boSung;
                    nguPhap.LuuY = luuY;
                    nguPhap.IdchuDeNguPhap = idChuDe;
                }

                return RedirectToAction("TopicDetail", "Grammar", new { idNguPhap = idChuDe });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }
    }
}
