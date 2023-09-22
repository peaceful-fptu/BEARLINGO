using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class NguPhap
{
    public int IdnguPhap { get; set; }

    public string? TieuDe { get; set; }

    public string? CachDung { get; set; }

    public string? CauTruc { get; set; }

    public string? ViDu { get; set; }

    public string? BoSung { get; set; }

    public string? LuuY { get; set; }

    public int IdchuDeNguPhap { get; set; }

    public virtual ChuDeNguPhap IdchuDeNguPhapNavigation { get; set; } = null!;
}
