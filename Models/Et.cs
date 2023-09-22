using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Et
{
    public int Idets { get; set; }

    public string? TenEts { get; set; }

    public int Idqtv { get; set; }

    public virtual ICollection<DeThi> DeThis { get; } = new List<DeThi>();

    public virtual Qtv IdqtvNavigation { get; set; } = null!;
}
