using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BEARLINGO.Models;

public partial class BearlingoContext : DbContext
{
    public BearlingoContext()
    {
    }

    public BearlingoContext(DbContextOptions<BearlingoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Audio> Audios { get; set; }

    public virtual DbSet<BaiLam> BaiLams { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<ChuDeNguPhap> ChuDeNguPhaps { get; set; }

    public virtual DbSet<ChuDeTuVung> ChuDeTuVungs { get; set; }

    public virtual DbSet<DeThi> DeThis { get; set; }

    public virtual DbSet<Et> Ets { get; set; }

    public virtual DbSet<KetQuaL> KetQuaLs { get; set; }

    public virtual DbSet<KetQuaR> KetQuaRs { get; set; }

    public virtual DbSet<Listening> Listenings { get; set; }

    public virtual DbSet<NguPhap> NguPhaps { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhanLoaiL> PhanLoaiLs { get; set; }

    public virtual DbSet<PhanLoaiR> PhanLoaiRs { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Qtv> Qtvs { get; set; }

    public virtual DbSet<Reading> Readings { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<TuVung> TuVungs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyBearlingo"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Audio>(entity =>
        {
            entity.HasKey(e => e.Idaudio).HasName("PK__Audio__59DA2EDA1304EF34");

            entity.ToTable("Audio");

            entity.Property(e => e.Idaudio).HasColumnName("IDAudio");
            entity.Property(e => e.IddeThi).HasColumnName("IDDeThi");

            entity.HasOne(d => d.IddeThiNavigation).WithMany(p => p.Audios)
                .HasForeignKey(d => d.IddeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Audio__IDDeThi__45F365D3");
        });

        modelBuilder.Entity<BaiLam>(entity =>
        {
            entity.HasKey(e => e.IdbaiLam).HasName("PK__BaiLam__CD7D499CD318B22B");

            entity.ToTable("BaiLam");

            entity.Property(e => e.IdbaiLam).HasColumnName("IDBaiLam");
            entity.Property(e => e.IddeThi).HasColumnName("IDDeThi");
            entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");
            entity.Property(e => e.NgayThi).HasColumnType("date");

            entity.HasOne(d => d.IddeThiNavigation).WithMany(p => p.BaiLams)
                .HasForeignKey(d => d.IddeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaiLam__IDDeThi__403A8C7D");

            entity.HasOne(d => d.IdnguoiDungNavigation).WithMany(p => p.BaiLams)
                .HasForeignKey(d => d.IdnguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaiLam__IDNguoiD__412EB0B6");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Idblog).HasName("PK__Blog__3CFB8BBEEAD003C4");

            entity.ToTable("Blog");

            entity.Property(e => e.Idblog).HasColumnName("IDBlog");
            entity.Property(e => e.Anh).HasMaxLength(255);
            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");

            entity.HasOne(d => d.IdqtvNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.Idqtv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blog__IDQTV__2E1BDC42");
        });

        modelBuilder.Entity<ChuDeNguPhap>(entity =>
        {
            entity.HasKey(e => e.IdchuDeNguPhap).HasName("PK__ChuDeNgu__37E44C4947A089D0");

            entity.ToTable("ChuDeNguPhap");

            entity.Property(e => e.IdchuDeNguPhap).HasColumnName("IDChuDeNguPhap");
            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");
            entity.Property(e => e.Stt).HasColumnName("STT");

            entity.HasOne(d => d.IdqtvNavigation).WithMany(p => p.ChuDeNguPhaps)
                .HasForeignKey(d => d.Idqtv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChuDeNguP__IDQTV__36B12243");
        });

        modelBuilder.Entity<ChuDeTuVung>(entity =>
        {
            entity.HasKey(e => e.IdchuDeTuVung).HasName("PK__ChuDeTuV__15A303D8092F2D24");

            entity.ToTable("ChuDeTuVung");

            entity.Property(e => e.IdchuDeTuVung).HasColumnName("IDChuDeTuVung");
            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");
            entity.Property(e => e.Stt).HasColumnName("STT");

            entity.HasOne(d => d.IdqtvNavigation).WithMany(p => p.ChuDeTuVungs)
                .HasForeignKey(d => d.Idqtv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChuDeTuVu__IDQTV__30F848ED");
        });

        modelBuilder.Entity<DeThi>(entity =>
        {
            entity.HasKey(e => e.IddeThi).HasName("PK__DeThi__18DD254977388BB7");

            entity.ToTable("DeThi");

            entity.Property(e => e.IddeThi).HasColumnName("IDDeThi");
            entity.Property(e => e.Idets).HasColumnName("IDETS");
            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");

            entity.HasOne(d => d.IdetsNavigation).WithMany(p => p.DeThis)
                .HasForeignKey(d => d.Idets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeThi__IDETS__3D5E1FD2");

            entity.HasOne(d => d.IdqtvNavigation).WithMany(p => p.DeThis)
                .HasForeignKey(d => d.Idqtv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeThi__IDQTV__3C69FB99");
        });

        modelBuilder.Entity<Et>(entity =>
        {
            entity.HasKey(e => e.Idets).HasName("PK__ETS__9228B3476B54FBEC");

            entity.ToTable("ETS");

            entity.Property(e => e.Idets).HasColumnName("IDETS");
            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");
            entity.Property(e => e.TenEts)
                .HasMaxLength(255)
                .HasColumnName("TenETS");

            entity.HasOne(d => d.IdqtvNavigation).WithMany(p => p.Ets)
                .HasForeignKey(d => d.Idqtv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ETS__IDQTV__286302EC");
        });

        modelBuilder.Entity<KetQuaL>(entity =>
        {
            entity.HasKey(e => e.IdketQuaL).HasName("PK__KetQuaL__6D3D3866C1C2BAE9");

            entity.ToTable("KetQuaL");

            entity.Property(e => e.IdketQuaL).HasColumnName("IDKetQuaL");
            entity.Property(e => e.CorrectL).HasMaxLength(50);
            entity.Property(e => e.IdbaiLam).HasColumnName("IDBaiLam");
            entity.Property(e => e.Idlquestion).HasColumnName("IDLQuestion");
            entity.Property(e => e.NgayThi).HasColumnType("date");

            entity.HasOne(d => d.IdbaiLamNavigation).WithMany(p => p.KetQuaLs)
                .HasForeignKey(d => d.IdbaiLam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KetQuaL__IDBaiLa__59063A47");

            entity.HasOne(d => d.IdlquestionNavigation).WithMany(p => p.KetQuaLs)
                .HasForeignKey(d => d.Idlquestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KetQuaL__IDLQues__5812160E");
        });

        modelBuilder.Entity<KetQuaR>(entity =>
        {
            entity.HasKey(e => e.IdketQuaR).HasName("PK__KetQuaR__6D3D385CE38CA5E4");

            entity.ToTable("KetQuaR");

            entity.Property(e => e.IdketQuaR).HasColumnName("IDKetQuaR");
            entity.Property(e => e.CorrectR).HasMaxLength(50);
            entity.Property(e => e.IdbaiLam).HasColumnName("IDBaiLam");
            entity.Property(e => e.Idrquestion).HasColumnName("IDRQuestion");
            entity.Property(e => e.NgayThi).HasColumnType("date");

            entity.HasOne(d => d.IdbaiLamNavigation).WithMany(p => p.KetQuaRs)
                .HasForeignKey(d => d.IdbaiLam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KetQuaR__IDBaiLa__5CD6CB2B");

            entity.HasOne(d => d.IdrquestionNavigation).WithMany(p => p.KetQuaRs)
                .HasForeignKey(d => d.Idrquestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KetQuaR__IDRQues__5BE2A6F2");
        });

        modelBuilder.Entity<Listening>(entity =>
        {
            entity.HasKey(e => e.Idlquestion).HasName("PK__Listenin__E1F3BA46FE01AB5F");

            entity.ToTable("Listening");

            entity.Property(e => e.Idlquestion).HasColumnName("IDLQuestion");
            entity.Property(e => e.Idaudio).HasColumnName("IDAudio");
            entity.Property(e => e.IddeThi).HasColumnName("IDDeThi");
            entity.Property(e => e.IdphanLoaiL).HasColumnName("IDPhanLoaiL");
            entity.Property(e => e.Idpicture).HasColumnName("IDPicture");
            entity.Property(e => e.Stt).HasColumnName("STT");

            entity.HasOne(d => d.IdaudioNavigation).WithMany(p => p.Listenings)
                .HasForeignKey(d => d.Idaudio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listening__IDAud__4D94879B");

            entity.HasOne(d => d.IddeThiNavigation).WithMany(p => p.Listenings)
                .HasForeignKey(d => d.IddeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listening__IDDeT__4BAC3F29");

            entity.HasOne(d => d.IdphanLoaiLNavigation).WithMany(p => p.Listenings)
                .HasForeignKey(d => d.IdphanLoaiL)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listening__IDPha__4CA06362");

            entity.HasOne(d => d.IdpictureNavigation).WithMany(p => p.Listenings)
                .HasForeignKey(d => d.Idpicture)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listening__IDPic__4E88ABD4");
        });

        modelBuilder.Entity<NguPhap>(entity =>
        {
            entity.HasKey(e => e.IdnguPhap).HasName("PK__NguPhap__CA382F828A26A02F");

            entity.ToTable("NguPhap");

            entity.Property(e => e.IdnguPhap).HasColumnName("IDNguPhap");
            entity.Property(e => e.IdchuDeNguPhap).HasColumnName("IDChuDeNguPhap");

            entity.HasOne(d => d.IdchuDeNguPhapNavigation).WithMany(p => p.NguPhaps)
                .HasForeignKey(d => d.IdchuDeNguPhap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NguPhap__IDChuDe__398D8EEE");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.IdnguoiDung).HasName("PK__NguoiDun__FCD7DB0965E4F7E8");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.IdnguoiDung).HasColumnName("IDNguoiDung");
            entity.Property(e => e.Gmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sdt).HasColumnName("SDT");
            entity.Property(e => e.TenDangNhap).HasMaxLength(255);
        });

        modelBuilder.Entity<PhanLoaiL>(entity =>
        {
            entity.HasKey(e => e.IdphanLoaiL).HasName("PK__PhanLoai__C0851AC030D020D1");

            entity.ToTable("PhanLoaiL");

            entity.Property(e => e.IdphanLoaiL).HasColumnName("IDPhanLoaiL");
        });

        modelBuilder.Entity<PhanLoaiR>(entity =>
        {
            entity.HasKey(e => e.IdphanLoaiR).HasName("PK__PhanLoai__C0851D3AEEDA15AE");

            entity.ToTable("PhanLoaiR");

            entity.Property(e => e.IdphanLoaiR).HasColumnName("IDPhanLoaiR");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.Idpicture).HasName("PK__Picture__C37C3FD19C5FF7AE");

            entity.ToTable("Picture");

            entity.Property(e => e.Idpicture).HasColumnName("IDPicture");
            entity.Property(e => e.IddeThi).HasColumnName("IDDeThi");

            entity.HasOne(d => d.IddeThiNavigation).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.IddeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Picture__IDDeThi__48CFD27E");
        });

        modelBuilder.Entity<Qtv>(entity =>
        {
            entity.HasKey(e => e.Idqtv).HasName("PK__QTV__A74052336123A937");

            entity.ToTable("QTV");

            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");
            entity.Property(e => e.Gmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenDangNhap).HasMaxLength(255);
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.HasKey(e => e.Idrquestion).HasName("PK__Reading__2C13B396986FC12C");

            entity.ToTable("Reading");

            entity.Property(e => e.Idrquestion).HasColumnName("IDRQuestion");
            entity.Property(e => e.IddeThi).HasColumnName("IDDeThi");
            entity.Property(e => e.IdphanLoaiR).HasColumnName("IDPhanLoaiR");
            entity.Property(e => e.Idpicture).HasColumnName("IDPicture");
            entity.Property(e => e.Stt).HasColumnName("STT");

            entity.HasOne(d => d.IddeThiNavigation).WithMany(p => p.Readings)
                .HasForeignKey(d => d.IddeThi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reading__IDDeThi__534D60F1");

            entity.HasOne(d => d.IdphanLoaiRNavigation).WithMany(p => p.Readings)
                .HasForeignKey(d => d.IdphanLoaiR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reading__IDPhanL__5441852A");

            entity.HasOne(d => d.IdpictureNavigation).WithMany(p => p.Readings)
                .HasForeignKey(d => d.Idpicture)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reading__IDPictu__5535A963");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.Idtips).HasName("PK__Tips__BEB088A2FD3C9164");

            entity.Property(e => e.Idtips).HasColumnName("IDTips");
            entity.Property(e => e.Idqtv).HasColumnName("IDQTV");

            entity.HasOne(d => d.IdqtvNavigation).WithMany(p => p.Tips)
                .HasForeignKey(d => d.Idqtv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tips__IDQTV__2B3F6F97");
        });

        modelBuilder.Entity<TuVung>(entity =>
        {
            entity.HasKey(e => e.IdtuVung).HasName("PK__TuVung__3D5AF069BF66DF17");

            entity.ToTable("TuVung");

            entity.Property(e => e.IdtuVung).HasColumnName("IDTuVung");
            entity.Property(e => e.IdchuDeTuVung).HasColumnName("IDChuDeTuVung");
            entity.Property(e => e.LoaiTu)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PhatAm).IsUnicode(false);
            entity.Property(e => e.TuVung1)
                .IsUnicode(false)
                .HasColumnName("TuVung");

            entity.HasOne(d => d.IdchuDeTuVungNavigation).WithMany(p => p.TuVungs)
                .HasForeignKey(d => d.IdchuDeTuVung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TuVung__IDChuDeT__33D4B598");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
