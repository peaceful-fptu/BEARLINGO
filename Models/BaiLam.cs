using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class BaiLam
{
    public int IdbaiLam { get; set; }

    public TimeSpan? ThoiGian { get; set; }

    public DateTime? NgayThi { get; set; }

    public int IddeThi { get; set; }

    public int IdnguoiDung { get; set; }

    public virtual DeThi IddeThiNavigation { get; set; } = null!;

    public virtual NguoiDung IdnguoiDungNavigation { get; set; } = null!;

    public virtual ICollection<KetQuaL> KetQuaLs { get; } = new List<KetQuaL>();

    public virtual ICollection<KetQuaR> KetQuaRs { get; } = new List<KetQuaR>();
}
