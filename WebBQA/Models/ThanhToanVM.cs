using System.ComponentModel.DataAnnotations;

namespace WebBQA.Models
{
    public class ThanhToanVM
    {
        [Required(ErrorMessage="Bạn không được để trống")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string? HoTen {  get; set; }
        [Required(ErrorMessage = "Bạn không được để trống")]
        [MaxLength(256, ErrorMessage = "Tối đa 256 kí tự")]
        public string? DiaChi { get; set; }
        [Required(ErrorMessage = "Bạn không được để trống")]
        [MaxLength(25, ErrorMessage = "Tối đa 25 kí tự")]

        public string? DienThoai { get; set; }
        public string? GhiChu { get; set; }

    }
}
