namespace WebBQA.Models
{
    public class HoaDonql
    {
        public string MaHoaDon { get; set; } = null!;

        public DateTime? NgayHoaDon { get; set; }

        public string? MaKhachHang { get; set; }

        public int? TongTienHd { get; set; }

        public string? PhuongThucThanhToan { get; set; }

        public string? MaTrangThai { get; set; }

        public string? SoDienThoai { get; set; }

        public string? DiaChi { get; set; }

        public string TenNguoiNhan { get; set; } = null!;

        public string? GhiChu { get; set; }

        public string MaSp { get; set; } = null!;

        public int? SoLuongBan { get; set; }

        public int? DonGiaBan { get; set; }

        public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    }
}
