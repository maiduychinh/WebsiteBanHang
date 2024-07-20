using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBQA.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using X.PagedList;
using WebBQA.Models.Authentication;
namespace WebBQA.Controllers
{
 
        public class HomeController : Controller

        {

            QuanLyQaContext db = new QuanLyQaContext();

            private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }
            [Authentication]
            public IActionResult Index(int? page)
            {
                int pageSize = 8;
                int pageNumber = page == null || page < 0 ? 1 : page.Value;

                var lstsanpham = db.DanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
                PagedList<DanhMucSp> lst = new PagedList<DanhMucSp>(lstsanpham, pageNumber, pageSize);
                return View(lst);

            }
            public IActionResult SanPhamTheoLoai(String maloai)
            {
                List<DanhMucSp> lstsanpham = db.DanhMucSps.Where
                    (x => x.MaLoai == maloai).OrderBy(x => x.TenSp).ToList();
                return View(lstsanpham);
            }

            public IActionResult ChiTietSanPham(String maSp)
            {
                var sanPham = db.DanhMucSps.SingleOrDefault(x => x.MaSp == maSp);



                var chatL = db.ChatLieus.FirstOrDefault(x => x.MaChatLieu == sanPham.MaChatLieu);
                var hangSx = db.HangSxes.FirstOrDefault(x => x.MaHangSx == sanPham.MaHangSx);
                var nuocSx = db.QuocGia.FirstOrDefault(x => x.MaNuocSx == sanPham.MaNuocSx);
                var loaiSp = db.LoaiSps.FirstOrDefault(x => x.MaLoai == sanPham.MaLoai);
                var kichThuoc = db.TKichThuocs.FirstOrDefault(x => x.MaKichThuoc == sanPham.MaKichThuoc);
                var mauSac = db.MauSacs.FirstOrDefault(x => x.MaMauSac == sanPham.MaMauSac);



                ViewBag.MaChatLieu = chatL.ChatLieu1;
                ViewBag.MaHangSx = hangSx.HangSx1;
                ViewBag.MaNuocSx = nuocSx.TenNuoc;
                ViewBag.MaLoaiSp = loaiSp.Loai;
                ViewBag.MaKichThuoc =kichThuoc.KichThuoc;
                ViewBag.MaMauSac = mauSac.TenMauSac;

                var anhSanPham = db.AnhSps.Where(x => x.MaSp == maSp).ToList();
                ViewBag.anhSanPham = anhSanPham;






            return View(sanPham);

            }



        public IActionResult TimKiem(string keyword, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstsanpham = db.DanhMucSps
                .AsNoTracking()
                .Where(x => x.TenSp.Contains(keyword))
                .OrderBy(x => x.TenSp);

            PagedList<DanhMucSp> lst = new PagedList<DanhMucSp>(lstsanpham, pageNumber, pageSize);
            ViewBag.Keyword = keyword; // ?? gi? l?i t? khóa tìm ki?m trên giao di?n
            return View("Index", lst);


        }
        public IActionResult XemDonHang()
        {
            var maKhachHang = HttpContext.Session.GetString("MaKhachHang");

            if (string.IsNullOrEmpty(maKhachHang))
            {
                // N?u khách hàng ch?a ??ng nh?p, chuy?n h??ng ??n trang ??ng nh?p ho?c x? lý phù h?p
                return RedirectToAction("Login", "Access");
            }

            var donHangs = db.HoaDonBans
                            .Include(e => e.MaTrangThaiNavigation)
                            .Include(h => h.ChiTietHdbs)
                            .ThenInclude(ct => ct.MaSpNavigation)
                            .Where(h => h.MaKhachHang == maKhachHang)
                            .ToList();

            return View(donHangs);
        }
        public IActionResult ChiTietDonHang(string maHoaDon)
        {
            var donHang = db.HoaDonBans

                            .Include(e => e.MaTrangThaiNavigation)
                            .Include(e => e.MaKhachHangNavigation)
                            .Include(h => h.ChiTietHdbs)
                            .ThenInclude(ct => ct.MaSpNavigation)
                            .FirstOrDefault(h => h.MaHoaDon == maHoaDon);

            if (donHang == null)
            {
                return NotFound(); // Tr? v? l?i 404 n?u không tìm th?y ??n hàng
            }

            return View(donHang);
        }










        public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    

}
