using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBQA.Models;

public partial class QuanLyQaContext : DbContext
{
    public QuanLyQaContext()
    {
    }

    public QuanLyQaContext(DbContextOptions<QuanLyQaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSp> AnhSps { get; set; }

    public virtual DbSet<ChatLieu> ChatLieus { get; set; }

    public virtual DbSet<ChiTietHdb> ChiTietHdbs { get; set; }

    public virtual DbSet<DanhMucSp> DanhMucSps { get; set; }

    public virtual DbSet<HangSx> HangSxes { get; set; }

    public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiSp> LoaiSps { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<QuocGia> QuocGia { get; set; }

    public virtual DbSet<TKichThuoc> TKichThuocs { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-25L0OQD\\SQLEXPRESS;Initial Catalog=QuanLyQA;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhSp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AnhSp");

            entity.Property(e => e.MaSp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenFileAnh).HasMaxLength(50);
        });

        modelBuilder.Entity<ChatLieu>(entity =>
        {
            entity.HasKey(e => e.MaChatLieu);

            entity.ToTable("ChatLieu");

            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ChatLieu1)
                .HasMaxLength(150)
                .HasColumnName("ChatLieu");
        });

        modelBuilder.Entity<ChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.MaHoaDon, e.MaSp });

            entity.ToTable("ChiTietHDB");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaSp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.GhiChu)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_HoaDonBan");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_DanhMucSP");
        });

        modelBuilder.Entity<DanhMucSp>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("DanhMucSP");

            entity.Property(e => e.MaSp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaSP");
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GioiThieuSp)
                .HasMaxLength(255)
                .HasColumnName("GioiThieuSP");
            entity.Property(e => e.MaChatLieu)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaHangSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHangSX");
            entity.Property(e => e.MaKichThuoc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaLoai)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaMauSac)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaNuocSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNuocSX");
            entity.Property(e => e.Slton).HasColumnName("SLTon");
            entity.Property(e => e.TenSp)
                .HasMaxLength(150)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaChatLieuNavigation).WithMany(p => p.DanhMucSps)
                .HasForeignKey(d => d.MaChatLieu)
                .HasConstraintName("FK_DanhMucSP_ChatLieu");

            entity.HasOne(d => d.MaHangSxNavigation).WithMany(p => p.DanhMucSps)
                .HasForeignKey(d => d.MaHangSx)
                .HasConstraintName("FK_DanhMucSP_HangSX");

            entity.HasOne(d => d.MaKichThuocNavigation).WithMany(p => p.DanhMucSps)
                .HasForeignKey(d => d.MaKichThuoc)
                .HasConstraintName("FK_DanhMucSP_tKichThuoc");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.DanhMucSps)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_DanhMucSP_LoaiSP");

            entity.HasOne(d => d.MaMauSacNavigation).WithMany(p => p.DanhMucSps)
                .HasForeignKey(d => d.MaMauSac)
                .HasConstraintName("FK_DanhMucSP_MauSac");

           entity.HasOne(d => d.MaNuocSxNavigation).WithMany(p => p.DanhMucSps)
                .HasForeignKey(d => d.MaNuocSx)
                .HasConstraintName("FK_DanhMucSP_QuocGia1");
        });

        modelBuilder.Entity<HangSx>(entity =>
        {
            entity.HasKey(e => e.MaHangSx);

            entity.ToTable("HangSX");

            entity.Property(e => e.MaHangSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHangSX");
            entity.Property(e => e.HangSx1)
                .HasMaxLength(100)
                .HasColumnName("HangSX");
        });

        modelBuilder.Entity<HoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon);

            entity.ToTable("HoaDonBan");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DiaChi).HasMaxLength(256);
            entity.Property(e => e.GhiChu).HasMaxLength(256);
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaTrangThai).HasMaxLength(100);
            entity.Property(e => e.NgayHoaDon).HasColumnType("datetime");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNguoiNhan).HasMaxLength(50);
            entity.Property(e => e.TongTienHd).HasColumnName("TongTienHD");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK_HoaDonBan_KhachHang");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaTrangThai)
                .HasConstraintName("FK_HoaDonBan_TrangThai");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang);

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DiaChi).HasMaxLength(150);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
        });

        modelBuilder.Entity<LoaiSp>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiSP");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Loai).HasMaxLength(100);
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.HasKey(e => e.MaMauSac);

            entity.ToTable("MauSac");

            entity.Property(e => e.MaMauSac)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenMauSac).HasMaxLength(100);
        });

        modelBuilder.Entity<QuocGia>(entity =>
        {
            entity.HasKey(e => e.MaNuocSx).HasName("PK_QuocGia_1");

            entity.Property(e => e.MaNuocSx)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNuoc).HasMaxLength(100);
        });

        modelBuilder.Entity<TKichThuoc>(entity =>
        {
            entity.HasKey(e => e.MaKichThuoc);

            entity.ToTable("tKichThuoc");

            entity.Property(e => e.MaKichThuoc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.KichThuoc)
                .HasMaxLength(150)
                .IsFixedLength();
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai);

            entity.ToTable("TrangThai");

            entity.Property(e => e.MaTrangThai).HasMaxLength(100);
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
