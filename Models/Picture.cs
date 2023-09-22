using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Picture
{
    public int Idpicture { get; set; }

    public string? TenFile { get; set; }

    public string? GhiChu { get; set; }

    public int IddeThi { get; set; }

    public virtual DeThi IddeThiNavigation { get; set; } = null!;

    public virtual ICollection<Listening> Listenings { get; } = new List<Listening>();

    public virtual ICollection<Reading> Readings { get; } = new List<Reading>();
}
