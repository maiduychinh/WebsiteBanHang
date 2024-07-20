using System;
using System.Collections.Generic;

namespace WebBQA.Models;

public partial class ChiTietHdb
{
    public string MaHoaDon { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public int? SoLuongBan { get; set; }

    public int? DonGiaBan { get; set; }

    public string? GhiChu { get; set; }

    public virtual HoaDonBan MaHoaDonNavigation { get; set; } = null!;

    public virtual DanhMucSp MaSpNavigation { get; set; } = null!;
}
