﻿using BEARLINGO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static BEARLINGO.Program;

namespace BEARLINGO.Controllers.Admin
{
    public class VocabularyController : Controller
    {
        private readonly BearlingoContext _context;

        public VocabularyController()
        {
            _context = new BearlingoContext();
        }

        [HttpGet]
        public IActionResult Vocabulary(string num)
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
            ViewData["listChuDeTuVung"] = listChuDe;
            ViewData["totalPages"] = totalPages;
            return View();
        }

        [HttpGet]
        public IActionResult VocabularyDetail(int id)
        {
            var listTuVung = this.GetTuVung(id);
            ViewData["listTuVung"] = listTuVung;
            return View();
        }

        public List<ChuDeTuVung> GetChuDe()
        {
            var result = from chude in this._context.ChuDeTuVungs
                         join qtv in this._context.Qtvs
                            on chude.Idqtv equals qtv.Idqtv
                         select new ChuDeTuVung
                         {
                             IdchuDeTuVung = chude.IdchuDeTuVung,
                             ChuDe = chude.ChuDe,
                             Stt = chude.Stt,
                             Idqtv = qtv.Idqtv,
                         };

            if (result != null)
            {
                return result.ToList();
            }

            return new List<ChuDeTuVung>();
        }

        public List<TuVung> GetTuVung(int idChuDe)
        {
            var result = from tuvung in this._context.TuVungs
                         join chude in this._context.ChuDeTuVungs
                            on tuvung.IdchuDeTuVung equals chude.IdchuDeTuVung
                         where tuvung.IdchuDeTuVung == idChuDe
                         select new TuVung
                         {
                             IdtuVung = tuvung.IdtuVung,
                             TuVung1 = tuvung.TuVung1,
                             PhatAm = tuvung.PhatAm,
                             LoaiTu = tuvung.LoaiTu,
                             NghiaTuVung = tuvung.NghiaTuVung,
                             ViDuTuVung = tuvung.ViDuTuVung,
                             IdchuDeTuVung = tuvung.IdchuDeTuVung,
                         };

            if (result != null)
            {
                return result.ToList();
            }

            return new List<TuVung>();
        }

        [Authorize(Policy = Roles.Admin)]
        [HttpPost]
        public IActionResult AddChuDe(string chuDe, int stt, int idQtv)
        {
            try
            {
                ChuDeTuVung chuDeTuVung = new ChuDeTuVung
                {
                    ChuDe = chuDe,
                    Stt = stt,
                    Idqtv = idQtv,
                };

                this._context.ChuDeTuVungs.Add(chuDeTuVung);
                this._context.SaveChanges();

                return RedirectToAction("Vocabulary", "Vocabulary");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
        [HttpPost]
        public IActionResult AddTuVung(string tuVung1, string phatAm, string loaiTu, string nghiaTuVung, string viDuTuVung, int idChuDeTuVung)
        {
            try
            {
                TuVung tuVung = new TuVung
                {
                    TuVung1 = tuVung1,
                    PhatAm = phatAm,
                    LoaiTu = loaiTu,
                    NghiaTuVung = nghiaTuVung,
                    ViDuTuVung = viDuTuVung,
                    IdchuDeTuVung = idChuDeTuVung,
                };

                this._context.TuVungs.Add(tuVung);
                this._context.SaveChanges();

                return RedirectToAction("VocabularyDetail", "Vocabulary", new { idChuDe = idChuDeTuVung });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
        [HttpGet]
        public IActionResult DeleteChuDeTuVung(int idChuDeTuVung)
        {
            var chuDeTuVung = this._context.ChuDeTuVungs.FirstOrDefault(item => item.IdchuDeTuVung == idChuDeTuVung);
            var tuVung = this._context.TuVungs.Where(item => item.IdchuDeTuVung == idChuDeTuVung);

            using (var transaction = this._context.Database.BeginTransaction())
            {
                try
                {
                    if (tuVung != null)
                    {
                        foreach (var item in tuVung)
                        {
                            this._context.TuVungs.Remove(item);
                            this._context.SaveChanges();
                        }
                    }

                    if (chuDeTuVung != null)
                    {
                        this._context.ChuDeTuVungs.Remove(chuDeTuVung);
                        this._context.SaveChanges();
                    }

                    transaction.Commit();

                    return RedirectToAction("Vocabulary", "Vocabulary");
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
        public IActionResult DeleteTuVung(int idTuVung, int idChuDeTuVung)
        {
            var tuVung = this._context.TuVungs.FirstOrDefault(item => item.IdtuVung == idTuVung);
            try
            {
                if (tuVung != null)
                {
                    this._context.Remove(tuVung);
                    this._context.SaveChanges();
                }

                return RedirectToAction("VocabularyDetail", "Vocabulary", new { idChuDe = idChuDeTuVung });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
        [HttpPost]
        public IActionResult UpdateChuDeTuVung(int idChuDeTuVung, string chuDe, int stt, int idQtv)
        {
            var chuDeTuVung = this._context.ChuDeTuVungs.FirstOrDefault(item => item.IdchuDeTuVung == idChuDeTuVung);
            try
            {
                if (chuDeTuVung != null)
                {
                    chuDeTuVung.ChuDe = chuDe;
                    chuDeTuVung.Stt = stt;
                    chuDeTuVung.Idqtv = idQtv;
                }

                return RedirectToAction("Vocabulary", "Vocabulary");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }

        [Authorize(Policy = Roles.Admin)]
        [HttpPost]
        public IActionResult UpdateTuVung(int idTuVung, string tuVung1, string phatAm, string loaiTu, string nghiaTuVung, string viDuTuVung, int idChuDeTuVung)
        {
            var tuVung = this._context.TuVungs.FirstOrDefault(item => item.IdtuVung == idTuVung);
            try
            {
                if (tuVung != null)
                {
                    tuVung.TuVung1 = tuVung1;
                    tuVung.PhatAm = phatAm;
                    tuVung.LoaiTu = loaiTu;
                    tuVung.NghiaTuVung = nghiaTuVung;
                    tuVung.ViDuTuVung = viDuTuVung;
                    tuVung.IdchuDeTuVung = idChuDeTuVung;
                }

                return RedirectToAction("VocabularyDetail", "Vocabulary", new { idChuDe = idChuDeTuVung });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }
    }
}
