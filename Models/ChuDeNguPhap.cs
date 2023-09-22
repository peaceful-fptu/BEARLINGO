using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class ChuDeNguPhap
{
    public int IdchuDeNguPhap { get; set; }

    public string? TenNguPhap { get; set; }

    public int? Stt { get; set; }

    public int Idqtv { get; set; }

    public virtual Qtv IdqtvNavigation { get; set; } = null!;

    public virtual ICollection<NguPhap> NguPhaps { get; } = new List<NguPhap>();
}
