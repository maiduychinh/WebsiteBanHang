namespace WebBQA.Models
{
    public class CartItem
    {
        public string? MaSp { get; set; }
        public string? TenSp { get; set; }
        public string? AnhDaiDien { get; set; }
        public int GiaSanPham { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien => SoLuong * GiaSanPham;
        public int TongTien { get; set; }
    }
}
