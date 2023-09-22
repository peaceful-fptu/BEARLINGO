using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Tip
{
    public int Idtips { get; set; }

    public string? TieuDe { get; set; }

    public string? NoiDungTips { get; set; }

    public string? Anh { get; set; }

    public int Idqtv { get; set; }

    public virtual Qtv IdqtvNavigation { get; set; } = null!;
}
