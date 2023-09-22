using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Blog
{
    public int Idblog { get; set; }

    public string? TieuDe { get; set; }

    public string? NoiDungBlog { get; set; }

    public string? Anh { get; set; }

    public int Idqtv { get; set; }

    public virtual Qtv IdqtvNavigation { get; set; } = null!;
}
