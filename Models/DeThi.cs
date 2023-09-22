using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class DeThi
{
    public int IddeThi { get; set; }

    public string? TenDeThi { get; set; }

    public TimeSpan? ThoiGian { get; set; }

    public string? NoiDungDeThi { get; set; }

    public int Idqtv { get; set; }

    public int Idets { get; set; }

    public virtual ICollection<Audio> Audios { get; } = new List<Audio>();

    public virtual ICollection<BaiLam> BaiLams { get; } = new List<BaiLam>();

    public virtual Et IdetsNavigation { get; set; } = null!;

    public virtual Qtv IdqtvNavigation { get; set; } = null!;

    public virtual ICollection<Listening> Listenings { get; } = new List<Listening>();

    public virtual ICollection<Picture> Pictures { get; } = new List<Picture>();

    public virtual ICollection<Reading> Readings { get; } = new List<Reading>();
}
