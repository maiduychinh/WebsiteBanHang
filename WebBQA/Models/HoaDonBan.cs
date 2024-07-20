using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBQA.Models;

public partial class HoaDonBan
{
   
    public string MaHoaDon { get; set; } = null!;
  
    public DateTime? NgayHoaDon { get; set; }

    public string? MaKhachHang { get; set; }
    public int? TongTienHd { get; set; }

    public string? MaTrangThai { get; set; }
   
    public string? SoDienThoai { get; set; }
   
    public string TenNguoiNhan { get; set; } = null!;

    public string? GhiChu { get; set; }

    public string? PhuongThucThanhToan { get; set; }
    
    public string? DiaChi { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual TrangThai? MaTrangThaiNavigation { get; set; }
}
