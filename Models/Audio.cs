using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Audio
{
    public int Idaudio { get; set; }

    public string? TenFile { get; set; }

    public string? GhiChu { get; set; }

    public int IddeThi { get; set; }

    public virtual DeThi IddeThiNavigation { get; set; } = null!;

    public virtual ICollection<Listening> Listenings { get; } = new List<Listening>();
}
