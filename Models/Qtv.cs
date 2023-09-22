using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Qtv
{
    public int Idqtv { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? Gmail { get; set; }

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();

    public virtual ICollection<ChuDeNguPhap> ChuDeNguPhaps { get; } = new List<ChuDeNguPhap>();

    public virtual ICollection<ChuDeTuVung> ChuDeTuVungs { get; } = new List<ChuDeTuVung>();

    public virtual ICollection<DeThi> DeThis { get; } = new List<DeThi>();

    public virtual ICollection<Et> Ets { get; } = new List<Et>();

    public virtual ICollection<Tip> Tips { get; } = new List<Tip>();
}
