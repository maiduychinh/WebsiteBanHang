using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBQA.Models;

public partial class KhachHang
{
    public string MaKhachHang { get; set; } = null!;

    public string? Password { get; set; }

    public string? TenKhachHang { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? AnhDaiDien { get; set; }

    [Display(Name ="Loại tài khoản: 1.admin 0.khách hàng 2.khoá")]
    public int? LoaiUserr { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();
}
