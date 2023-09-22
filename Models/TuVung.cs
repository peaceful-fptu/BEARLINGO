using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class TuVung
{
    public int IdtuVung { get; set; }

    public string? TuVung1 { get; set; }

    public string? PhatAm { get; set; }

    public string? LoaiTu { get; set; }

    public string? NghiaTuVung { get; set; }

    public string? ViDuTuVung { get; set; }

    public int IdchuDeTuVung { get; set; }

    public virtual ChuDeTuVung IdchuDeTuVungNavigation { get; set; } = null!;
}
