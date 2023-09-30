using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class NguoiDung
{
    public int IdnguoiDung { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int Sdt { get; set; }

    public string? Gmail { get; set; }
    public int? IDDangNhap { get; set; }
    public virtual ICollection<BaiLam> BaiLams { get; } = new List<BaiLam>();
}
