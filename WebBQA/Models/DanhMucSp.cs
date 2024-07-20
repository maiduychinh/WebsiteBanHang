using System;
using System.Collections.Generic;

namespace WebBQA.Models;

public partial class DanhMucSp
{
    public string MaSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public string? MaChatLieu { get; set; }

    public string? MaHangSx { get; set; }

    public string? MaNuocSx { get; set; }

    public double? ThoiGianBaoHanh { get; set; }

    public string? GioiThieuSp { get; set; }

    public string? MaLoai { get; set; }

    public string? AnhDaiDien { get; set; }

    public int? GiaSanPham { get; set; }

    public string? MaKichThuoc { get; set; }

    public string? MaMauSac { get; set; }

    public int? Slton { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual ChatLieu? MaChatLieuNavigation { get; set; }

    public virtual HangSx? MaHangSxNavigation { get; set; }

    public virtual TKichThuoc? MaKichThuocNavigation { get; set; }

    public virtual LoaiSp? MaLoaiNavigation { get; set; }

    public virtual MauSac? MaMauSacNavigation { get; set; }

    public virtual QuocGia? MaNuocSxNavigation { get; set; }
}
