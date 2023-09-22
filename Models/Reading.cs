using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Reading
{
    public int Idrquestion { get; set; }

    public string? NoiDung { get; set; }

    public string? DapAn1 { get; set; }

    public string? DapAn2 { get; set; }

    public string? DapAn3 { get; set; }

    public string? DapAn4 { get; set; }

    public string? DapAnDung { get; set; }

    public string? GiaiThich { get; set; }

    public int? Stt { get; set; }

    public int IddeThi { get; set; }

    public int IdphanLoaiR { get; set; }

    public int Idpicture { get; set; }

    public virtual DeThi IddeThiNavigation { get; set; } = null!;

    public virtual PhanLoaiR IdphanLoaiRNavigation { get; set; } = null!;

    public virtual Picture IdpictureNavigation { get; set; } = null!;

    public virtual ICollection<KetQuaR> KetQuaRs { get; } = new List<KetQuaR>();
}
