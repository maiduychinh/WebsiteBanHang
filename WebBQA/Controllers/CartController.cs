using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBQA.Models;
using HienlthOnline.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Web.Helpers;





namespace WebBQA.Controllers
{
   
  

    public class CartController : Controller
    {
        QuanLyQaContext db = new QuanLyQaContext();

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        public IActionResult Index()
        {
            return View(Carts);
        }
        public IActionResult AddToCart(string id, int SoLuong, string type = "Normal")
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(x => x.MaSp == id);
            if (item == null) //chua co
            {
                var sp = db.DanhMucSps.SingleOrDefault(x => x.MaSp == id);
                item = new CartItem
                {
                    MaSp = id,
                    TenSp = sp.TenSp,
                    GiaSanPham = sp.GiaSanPham.Value,
                    SoLuong = SoLuong,
                    AnhDaiDien = sp.AnhDaiDien,
                    TongTien = Carts.Sum(p => p.ThanhTien)
                };

                myCart.Add(item);
            }
            else
            {
                item.SoLuong += SoLuong;
            }
            HttpContext.Session.Set("GioHang", myCart);

            if (type == "ajax")
            {
                return Json(new
                {
                    SoLuong = Carts.Sum(x => x.SoLuong)
                });
            }



            return RedirectToAction("Index");
        }

        // xoa gio hang
        public IActionResult RemoveFromCart(string id)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(x => x.MaSp == id);
            if (item != null)
            {
                myCart.Remove(item);
                HttpContext.Session.Set("GioHang", myCart);
            }

            return RedirectToAction("Index");
        }







        /*  [Authorize]*/
        [HttpGet]
        public IActionResult ThanhToan()
        {
          
            if (Carts.Count == 0) 
            {
                return RedirectToAction("index","Home");

            }

            return View(Carts);
        }
/*
      [Authorize]*/
        [HttpPost]
        public IActionResult ThanhToan(ThanhToanVM model,string kh)
        {
            TempData["Message"] = "";


            var mkh = HttpContext.Session.GetString("MaKhachHang");
            var khachhang = new KhachHang();

            khachhang = db.KhachHangs.SingleOrDefault(x => x.MaKhachHang == mkh);


                var random = new Random();
                var randomPart = random.Next(1000, 9999); // Generates a random 4-digit number
                var maHoaDon = $"HD{randomPart}";
                var hoadon = new HoaDonBan
                {
                    MaHoaDon = maHoaDon,
                    MaKhachHang = khachhang.MaKhachHang,
                    TenNguoiNhan = model.HoTen ?? khachhang.TenKhachHang,
                    DiaChi =model.DiaChi ?? khachhang.DiaChi,
                    SoDienThoai = model.DienThoai ?? khachhang.SoDienThoai,
                    NgayHoaDon = DateTime.Now,
                    PhuongThucThanhToan ="COD",
                    MaTrangThai="0",
                    GhiChu=model.GhiChu,
                    TongTienHd=Carts.Sum(x => x.ThanhTien),
                   

                };
                try
                {
                    db.HoaDonBans.Add(hoadon);
                    db.SaveChanges();

                var cthds = new List<ChiTietHdb>();
                foreach (var item in Carts)
                {
                    cthds.Add(new ChiTietHdb
                    {
                        MaHoaDon = hoadon.MaHoaDon,
                        SoLuongBan = item.SoLuong,
                        DonGiaBan = item.GiaSanPham,
                        MaSp = item.MaSp
                    });
                }
                db.AddRange(cthds);
                db.SaveChanges();



                TempData["Message"] = "Đặt hàng thành công!";
                return RedirectToAction("XemDonHang", "Home");

                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
 

            return View(Carts);
        }



    }
}
