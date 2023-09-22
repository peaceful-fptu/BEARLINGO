using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class PhanLoaiR
{
    public int IdphanLoaiR { get; set; }

    public string? Loai { get; set; }

    public virtual ICollection<Reading> Readings { get; } = new List<Reading>();
}
