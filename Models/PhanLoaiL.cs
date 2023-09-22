using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class PhanLoaiL
{
    public int IdphanLoaiL { get; set; }

    public string? Loai { get; set; }

    public string? TenAudio { get; set; }

    public virtual ICollection<Listening> Listenings { get; } = new List<Listening>();
}
