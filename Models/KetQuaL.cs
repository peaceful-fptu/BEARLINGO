using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class KetQuaL
{
    public int IdketQuaL { get; set; }

    public string? DapAnNguoiDungL { get; set; }

    public int? SoCauDung { get; set; }

    public int? SoCauSai { get; set; }

    public double? Diem { get; set; }

    public DateTime? NgayThi { get; set; }

    public TimeSpan? ThoiGian { get; set; }

    public int Idlquestion { get; set; }

    public int IdbaiLam { get; set; }

    public string? CorrectL { get; set; }

    public virtual BaiLam IdbaiLamNavigation { get; set; } = null!;

    public virtual Listening IdlquestionNavigation { get; set; } = null!;
}
