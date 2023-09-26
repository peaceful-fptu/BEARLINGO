using BEARLINGO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace BEARLINGO.Controllers.Admin
{
    public class ExamController : Controller
    {
        private readonly BearlingoContext _context;

        public ExamController()
        {
            _context = new BearlingoContext();
        }

        public IActionResult Exam()
        {
            var listDeThi = this.GetDeThi();
            ViewData["listDeThi"] = listDeThi;
            var listETS = this.GetETS();
            ViewData["listETS"] = listETS;
            return View();
        }

        public IActionResult ExamDetail(int idDeThi)
        {
            var exam = this.CreateExam(idDeThi);
            ViewData["exam"] = exam;
            return View();
        }

        public List<Et> GetETS()
        {
            var result = from ets in this._context.Ets
                         select ets;

            return result.ToList();
        }

        public List<DeThi> GetDeThi()
        {
            var result = from dethi in this._context.DeThis
                         join qtv in this._context.Qtvs
                            on dethi.Idqtv equals qtv.Idqtv
                         join ets in this._context.Ets
                            on dethi.Idets equals ets.Idets
                         select new DeThi
                         {
                             IddeThi = dethi.IddeThi,
                             TenDeThi = dethi.TenDeThi,
                             ThoiGian = dethi.ThoiGian,
                             NoiDungDeThi = dethi.NoiDungDeThi,
                             Idqtv = dethi.Idqtv,
                             Idets = dethi.Idets,
                             IdetsNavigation = ets,
                             IdqtvNavigation = qtv
                         };
            return result.ToList();
        }

        public Exam CreateExam(int idDeThi)
        {
            Random rand = new Random();
            Exam exam = new Exam();

            // Generate question for listening part 1
            exam.Part1 = new List<Listening>();
            int idPhanLoaiL = this.GetRandomIdPhanLoaiL("Part 1");
            var listListeningPart1 = this.GetListeningPart1(idDeThi);
            for (int i = 1; i <= 6; i++)
            {
                int index = rand.Next(0, listListeningPart1.Count);
                listListeningPart1[index].Stt = i;
                exam.Part1.Add(listListeningPart1[index]);
            }

            // Generate question for listening part 2
            exam.Part2 = new List<Listening>();
            idPhanLoaiL = this.GetRandomIdPhanLoaiL("Part 2");
            var listListeningPart2 = this.GetListeningPart2(idDeThi);
            for (int i = 7; i <= 31; i++)
            {
                int index = rand.Next(0, listListeningPart2.Count);
                listListeningPart2[index].Stt = i;
                exam.Part2.Add(listListeningPart2[index]);
            }

            // Generate question for listening part 3
            exam.Part3 = new List<Listening>();
            idPhanLoaiL = this.GetRandomIdPhanLoaiL("Part 3");
            var listListeningPart3 = this.GetListeningPart3(idDeThi);
            for (int i = 32; i <= 70; i++)
            {
                int index = rand.Next(0, listListeningPart3.Count);
                listListeningPart3[index].Stt = i;
                exam.Part3.Add(listListeningPart3[index]);
            }

            // Generate question for listening part 4
            exam.Part4 = new List<Listening>();
            idPhanLoaiL = this.GetRandomIdPhanLoaiL("Part 4");
            var listListeningPart4 = this.GetListeningPart4(idDeThi);
            for (int i = 71; i <= 100; i++)
            {
                int index = rand.Next(0, listListeningPart4.Count);
                listListeningPart4[index].Stt = i;
                exam.Part4.Add(listListeningPart4[index]);
            }

            // Generate question for reading part 5
            exam.Part5 = new List<Reading>();
            idPhanLoaiL = this.GetRandomIdPhanLoaiR("Part 5");
            var listReadingPart5 = this.GetListReadingsPart5(idDeThi);
            for (int i = 101; i <= 130; i++)
            {
                int index = rand.Next(0, listReadingPart5.Count);
                listReadingPart5[index].Stt = i;
                exam.Part5.Add(listReadingPart5[index]);
            }

            // Generate question for reading part 6
            exam.Part6 = new List<Reading>();
            idPhanLoaiL = this.GetRandomIdPhanLoaiR("Part 6");
            var listReadingPart6 = this.GetListReadingsPart6(idDeThi);
            for (int i = 131; i <= 140; i++)
            {
                int index = rand.Next(0, listReadingPart6.Count);
                listReadingPart6[index].Stt = i;
                exam.Part6.Add(listReadingPart6[index]);
            }

            // Generate question for reading part 7
            exam.Part7 = new List<Reading>();
            idPhanLoaiL = this.GetRandomIdPhanLoaiR("Part 7");
            var listReadingPart7 = this.GetListReadingsPart7(idDeThi);
            for (int i = 141; i <= 200; i++)
            {
                int index = rand.Next(0, listReadingPart7.Count);
                listReadingPart7[index].Stt = i;
                exam.Part7.Add(listReadingPart7[index]);
            }

            return exam;
        }

        public int GetRandomIdPhanLoaiL(string loai)
        {
            var listPhanLoaiL = from phanloai in this._context.PhanLoaiLs
                         where phanloai.Loai == loai
                         select phanloai.IdphanLoaiL;

            Random random = new Random();
            var result  = listPhanLoaiL.ToList();
            int index = random.Next(0, result.Count());
            return result[index];
        }
        public int GetRandomIdPhanLoaiR(string loai)
        {
            var listPhanLoaiR = from phanloai in this._context.PhanLoaiRs
                                where phanloai.Loai == loai
                                select phanloai.IdphanLoaiR;

            Random random = new Random();
            var result = listPhanLoaiR.ToList();
            int index = random.Next(0, result.Count());
            return result[index];
        }
        public List<Listening> GetListeningPart1(int idDeThi)
        {
            var result = from question in this._context.Listenings
                         join part in this._context.PhanLoaiLs
                         on question.IdphanLoaiL equals part.IdphanLoaiL
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         join audio in this._context.Audios
                         on question.Idaudio equals audio.Idaudio
                         where question.IddeThi == idDeThi && part.Loai == "Part 1"
                         select new Listening
                         {
                             Idlquestion = question.Idlquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiL = question.IdphanLoaiL,
                             Idpicture = question.Idpicture,
                             Idaudio = question.Idaudio,
                             IdaudioNavigation = audio,
                             IdpictureNavigation = picture,
                             IdphanLoaiLNavigation = part
                         };

            return result.ToList();
        }

        public List<Listening> GetListeningPart2(int idDeThi)
        {
            var result = from question in this._context.Listenings
                         join part in this._context.PhanLoaiLs
                         on question.IdphanLoaiL equals part.IdphanLoaiL
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         join audio in this._context.Audios
                         on question.Idaudio equals audio.Idaudio
                         where question.IddeThi == idDeThi && part.Loai == "Part 2"
                         select new Listening
                         {
                             Idlquestion = question.Idlquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiL = question.IdphanLoaiL,
                             Idpicture = question.Idpicture,
                             Idaudio = question.Idaudio,
                             IdaudioNavigation = audio,
                             IdpictureNavigation = picture,
                             IdphanLoaiLNavigation = part
                         };

            return result.ToList();
        }

        public List<Listening> GetListeningPart3(int idDeThi)
        {
            var result = from question in this._context.Listenings
                         join part in this._context.PhanLoaiLs
                         on question.IdphanLoaiL equals part.IdphanLoaiL
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         join audio in this._context.Audios
                         on question.Idaudio equals audio.Idaudio
                         where question.IddeThi == idDeThi && part.Loai == "Part 3"
                         select new Listening
                         {
                             Idlquestion = question.Idlquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiL = question.IdphanLoaiL,
                             Idpicture = question.Idpicture,
                             Idaudio = question.Idaudio,
                             IdaudioNavigation = audio,
                             IdpictureNavigation = picture,
                             IdphanLoaiLNavigation = part
                         };

            return result.ToList();
        }

        public List<Listening> GetListeningPart4(int idDeThi)
        {
            var result = from question in this._context.Listenings
                         join part in this._context.PhanLoaiLs
                         on question.IdphanLoaiL equals part.IdphanLoaiL
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         join audio in this._context.Audios
                         on question.Idaudio equals audio.Idaudio
                         where question.IddeThi == idDeThi && part.Loai == "Part 4"
                         select new Listening
                         {
                             Idlquestion = question.Idlquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiL = question.IdphanLoaiL,
                             Idpicture = question.Idpicture,
                             Idaudio = question.Idaudio,
                             IdaudioNavigation = audio,
                             IdpictureNavigation = picture,
                             IdphanLoaiLNavigation = part
                         };

            return result.ToList();
        }

        public List<Reading> GetListReadingsPart5(int idDeThi)
        {
            var result = from question in this._context.Readings
                         join part in this._context.PhanLoaiRs
                         on question.IdphanLoaiR equals part.IdphanLoaiR
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         where question.IddeThi == idDeThi && part.Loai == "Part 5"
                         select new Reading
                         {
                             Idrquestion = question.Idrquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiR = question.IdphanLoaiR,
                             Idpicture = question.Idpicture,
                             IdpictureNavigation = picture,
                             IdphanLoaiRNavigation = part
                         };

            return result.ToList();
        }

        public List<Reading> GetListReadingsPart6(int idDeThi)
        {
            var result = from question in this._context.Readings
                         join part in this._context.PhanLoaiRs
                         on question.IdphanLoaiR equals part.IdphanLoaiR
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         where question.IddeThi == idDeThi && part.Loai == "Part 6"
                         select new Reading
                         {
                             Idrquestion = question.Idrquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiR = question.IdphanLoaiR,
                             Idpicture = question.Idpicture,
                             IdpictureNavigation = picture,
                             IdphanLoaiRNavigation = part
                         };

            return result.ToList();
        }
        public List<Reading> GetListReadingsPart7(int idDeThi)
        {
            var result = from question in this._context.Readings
                         join part in this._context.PhanLoaiRs
                         on question.IdphanLoaiR equals part.IdphanLoaiR
                         join picture in this._context.Pictures
                         on question.Idpicture equals picture.Idpicture
                         where question.IddeThi == idDeThi && part.Loai == "Part 7"
                         select new Reading
                         {
                             Idrquestion = question.Idrquestion,
                             NoiDung = question.NoiDung,
                             DapAn1 = question.DapAn1,
                             DapAn2 = question.DapAn2,
                             DapAn3 = question.DapAn3,
                             DapAn4 = question.DapAn4,
                             DapAnDung = question.DapAnDung,
                             GiaiThich = question.GiaiThich,
                             Stt = question.Stt,
                             IddeThi = idDeThi,
                             IdphanLoaiR = question.IdphanLoaiR,
                             Idpicture = question.Idpicture,
                             IdpictureNavigation = picture,
                             IdphanLoaiRNavigation = part
                         };

            return result.ToList();
        }
    }
}
