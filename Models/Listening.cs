using System;
using System.Collections.Generic;

namespace BEARLINGO.Models;

public partial class Listening
{
    public int Idlquestion { get; set; }

    public string? NoiDung { get; set; }

    public string? DapAn1 { get; set; }

    public string? DapAn2 { get; set; }

    public string? DapAn3 { get; set; }

    public string? DapAn4 { get; set; }

    public string? DapAnDung { get; set; }

    public string? GiaiThich { get; set; }

    public int? Stt { get; set; }

    public int IddeThi { get; set; }

    public int IdphanLoaiL { get; set; }

    public int Idaudio { get; set; }

    public int Idpicture { get; set; }

    public virtual Audio IdaudioNavigation { get; set; } = null!;

    public virtual DeThi IddeThiNavigation { get; set; } = null!;

    public virtual PhanLoaiL IdphanLoaiLNavigation { get; set; } = null!;

    public virtual Picture IdpictureNavigation { get; set; } = null!;

    public virtual ICollection<KetQuaL> KetQuaLs { get; } = new List<KetQuaL>();
}
