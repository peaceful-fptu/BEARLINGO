using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class ChuDeTuVung
{
    public int IdchuDeTuVung { get; set; }

    public string? ChuDe { get; set; }

    public int? Stt { get; set; }

    public int Idqtv { get; set; }

    public virtual Qtv IdqtvNavigation { get; set; } = null!;

    public virtual ICollection<TuVung> TuVungs { get; } = new List<TuVung>();
}
