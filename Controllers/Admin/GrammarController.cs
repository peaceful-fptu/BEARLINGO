﻿using BEARLINGO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Drawing.Printing;
using static BEARLINGO.Program;

namespace BEARLINGO.Controllers.Admin
{
    public class GrammarController : Controller
    {
        private readonly BearlingoContext _context;

        public GrammarController()
        {
            _context = new BearlingoContext();
        }

        [HttpGet]
        public IActionResult Grammar(string num)
        {
            int totalPages = 0;
            int pageSize = 10;
            var listChuDe = this.GetChuDe();
            totalPages = (listChuDe.Count() % pageSize) > 0 ? (listChuDe.Count() / pageSize) + 1 : (listChuDe.Count() / pageSize);
            if (!string.IsNullOrEmpty(num))
            {
                listChuDe = this.GetChuDe().Skip(pageSize * (Convert.ToInt32(num) - 1)).Take(pageSize).ToList();
            }
            else
            {
                num = "1";
                listChuDe = this.GetChuDe().Skip(pageSize * (Convert.ToInt32(num) - 1)).Take(pageSize).ToList();
            }

            ViewData["listChuDeNguPhap"] = listChuDe;
            ViewData["totalPages"] = totalPages;
            return View();
        }

        [HttpGet]
        public IActionResult GrammarDetail(int idNguPhap)
        {
            var listNguPhap = this.GetNguPhap(idNguPhap);
            ViewData["listNguPhap"] = listNguPhap;
            return View();
        }

        public List<ChuDeNguPhap> GetChuDe()
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

        public List<NguPhap> GetNguPhap(int id)
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

        [Authorize(Policy = Roles.Admin)]
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
                return RedirectToAction("Grammar", "Grammar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
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

                return RedirectToAction("GrammarDetail", "Grammar", new { idNguPhap = idChuDe });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
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

                    return RedirectToAction("Grammar", "Grammar");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return View();
            }
        }

        [Authorize(Policy = Roles.Admin)]
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

                return RedirectToAction("GrammarDetail", "Grammar", new { idNguPhap = idNguPhap });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
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

                return RedirectToAction("Grammar", "Grammar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
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

                return RedirectToAction("GrammarDetail", "Grammar", new { idNguPhap = idChuDe });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }
    }
}
