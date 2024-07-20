using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBQA.Models;
using X.PagedList;

using System.Linq;

namespace WebBQA.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    public class HomeAdminController : Controller
    {
        QuanLyQaContext db = new QuanLyQaContext();
        [Route("")]
        [Route("index")]


        /*  public IActionResult Index()
        {
            return View();
        }*/
        // quản lý sản phảm
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page,String sanpham)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.DanhMucSps.
                Include(x => x.MaHangSxNavigation).
                Include(e=> e.MaChatLieuNavigation).
                Include(e => e.MaMauSacNavigation).


                AsNoTracking().OrderBy(x => x.TenSp);


            PagedList<DanhMucSp> lst = new PagedList<DanhMucSp>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(), "MaChatLieu", "ChatLieu1");
            ViewBag.MaHangSx = new SelectList(db.HangSxes.ToList(), "MaHangSx", "HangSx1");
            ViewBag.MaNuocSx = new SelectList(db.QuocGia.ToList(), "MaNuocSx", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaKichThuoc = new SelectList(db.TKichThuocs.ToList(), "MaKichThuoc", "KichThuoc");
            ViewBag.MaMauSac = new SelectList(db.MauSacs.ToList(), "MaMauSac", "TenMauSac");

            return View();

        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(DanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);

        }


        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(), "MaChatLieu", "ChatLieu1");
            ViewBag.MaHangSx = new SelectList(db.HangSxes.ToList(), "MaHangSx", "HangSx1");
            ViewBag.MaNuocSx = new SelectList(db.QuocGia.ToList(), "MaNuocSx", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaKichThuoc = new SelectList(db.TKichThuocs.ToList(), "MaKichThuoc", "KichThuoc");
            ViewBag.MaMauSac = new SelectList(db.MauSacs.ToList(), "MaMauSac", "TenMauSac");

            var sanPham = db.DanhMucSps.Find(maSanPham);
            return View(sanPham);

        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(DanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);

        }


        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            TempData["Message"] = "";


            db.Remove(db.DanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã đuoc xoá";

            return RedirectToAction("DanhMucSanPham", "HomeAdmin");

        }


        // QL tai khoan
        [Route("TaiKhoan")]
        public IActionResult TaiKhoan(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsttk = db.KhachHangs.AsNoTracking().OrderBy(x => x.MaKhachHang);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lsttk, pageNumber, pageSize);
            return View(lst);
        }


        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(string ma)
        {

            var taiKhoan = db.KhachHangs.Find(ma);
            return View(taiKhoan);

        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(KhachHang taiKhoan)
        {
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Sửa thành công";

                return RedirectToAction("TaiKhoan", "HomeAdmin");
            }
            return View(taiKhoan);

        }
      
        //quan ly Hoadon

        [Route("QuanLyHoaDon")]

        public IActionResult QuanlyHoaDon(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstHD = db.HoaDonBans.Include(e => e.MaTrangThaiNavigation).AsNoTracking().OrderBy(x => x.MaHoaDon);
            PagedList<HoaDonBan> lst = new PagedList<HoaDonBan>(lstHD, pageNumber, pageSize);
            return View(lst);



        }
        [Route("SuaHoaDon")]
        [HttpGet]
        public IActionResult SuaHoaDon(string maHD)
        {


            ViewBag.MaTrangThai = new SelectList(db.TrangThais.ToList(), "MaTrangThai", "TenTrangThai");

            var hoadon = db.HoaDonBans.Find(maHD);

            return View(hoadon);

        }
  

        [Route("SuaHoaDon")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHoaDon(HoaDonBan hoadon)
        {
            TempData["Message"] = "";
       
                db.Entry(hoadon).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Sửa thành công";
                return RedirectToAction("QuanLyHoaDon", "HomeAdmin");
            
  

        }

        //ql Nuocsx
        [Route("QuanLyNuoc")]

        public IActionResult QuanlyNuoc(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstnuoc = db.QuocGia.AsNoTracking().OrderBy(x => x.MaNuocSx);
            PagedList<QuocGia> lst = new PagedList<QuocGia>(lstnuoc, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemNuoc")]
        [HttpGet]
        public IActionResult ThemNuoc()
        {

            return View();

        }
        [Route("ThemNuoc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNuoc(QuocGia quocGia)
        {
            if (ModelState.IsValid)
            {
                db.QuocGia.Add(quocGia);
                db.SaveChanges();
                return RedirectToAction("QuanLyNuoc");
            }
            return View(quocGia);

        }






        //ql chatlieu
        [Route("QuanLyChatLieu")]

        public IActionResult QuanLyChatLieu(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstcl = db.ChatLieus.AsNoTracking().OrderBy(x => x.MaChatLieu);
            PagedList<ChatLieu> lst = new PagedList<ChatLieu>(lstcl, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemChatLieu")]
        [HttpGet]
        public IActionResult ThemChatLieu()
        {

            return View();

        }
        [Route("ThemChatLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChatLieu(ChatLieu cl)
        {
            if (ModelState.IsValid)
            {
                db.ChatLieus.Add(cl);
                db.SaveChanges();
                return RedirectToAction("QuanLyChatLieu");
            }
            return View(cl);

        }
        
        
        
        
        
        
        
        //ql loại Sp
        [Route("QuanLyLoaiSp")]

        public IActionResult QuanLyLoaiSp(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstcl = db.LoaiSps.AsNoTracking().OrderBy(x => x.MaLoai);
            PagedList<LoaiSp> lst = new PagedList<LoaiSp>(lstcl, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemLoaiSp")]
        [HttpGet]
        public IActionResult ThemLoaiSp()
        {

            return View();

        }
        [Route("ThemLoaiSp")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiSp(LoaiSp lSp)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSps.Add(lSp);
                db.SaveChanges();
                return RedirectToAction("QuanLyLoaiSp");
            }
            return View(lSp);

        }




        //ql Trạng thái
        [Route("QuanLyTrangThai")]

        public IActionResult QuanLyTrangThai(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstt = db.TrangThais.AsNoTracking().OrderBy(x => x.MaTrangThai);
            PagedList<TrangThai> lst = new PagedList<TrangThai>(lstt, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemTrangThai")]
        [HttpGet]
        public IActionResult ThemTrangThai()
        {

            return View();

        }
        [Route("ThemTrangThai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTrangThai(TrangThai ltt)
        {
            if (ModelState.IsValid)
            {
                db.TrangThais.Add(ltt);
                db.SaveChanges();
                return RedirectToAction("QuanLyTrangThai");
            }
            return View(ltt);

        }









        //ql Kích thước
        [Route("QuanLyKichThuoc")]

        public IActionResult QuanLyKichThuoc(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstt = db.TKichThuocs.AsNoTracking().OrderBy(x => x.MaKichThuoc);
            PagedList<TKichThuoc> lst = new PagedList<TKichThuoc>(lstt, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemKichThuoc")]
        [HttpGet]
        public IActionResult ThemKichThuoc()
        {

            return View();

        }
        [Route("ThemKichThuoc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKichThuoc(TKichThuoc ltt)
        {
            if (ModelState.IsValid)
            {
                db.TKichThuocs.Add(ltt);
                db.SaveChanges();
                return RedirectToAction("QuanLyKichThuoc");
            }
            return View(ltt);

        }






        //ql HangSx
        [Route("QuanLyHangSx")]

        public IActionResult QuanLyHangSx(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstt = db.HangSxes.AsNoTracking().OrderBy(x => x.MaHangSx);
            PagedList<HangSx> lst = new PagedList<HangSx>(lstt, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemHangSx")]
        [HttpGet]
        public IActionResult ThemHangSx()
        {

            return View();

        }
        [Route("ThemHangSx")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHangSx(HangSx ltt)
        {
            if (ModelState.IsValid)
            {
                db.HangSxes.Add(ltt);
                db.SaveChanges();
                return RedirectToAction("QuanLyHangSx");
            }
            return View(ltt);

        }

        //ql Mau
        [Route("QuanLyMau")]

        public IActionResult QuanLyMau(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstt = db.MauSacs.AsNoTracking().OrderBy(x => x.MaMauSac);
            PagedList<MauSac> lst = new PagedList<MauSac>(lstt, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemMau")]
        [HttpGet]
        public IActionResult ThemMau()
        {

            return View();

        }
        [Route("ThemMau")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMau(MauSac ltt)
        {
            if (ModelState.IsValid)
            {
                db.MauSacs.Add(ltt);
                db.SaveChanges();
                return RedirectToAction("QuanLyMau");
            }
            return View(ltt);

        }

    





        // sanphambanchay
        [Route("SanPhamBanChay")]
        public IActionResult SanPhamBanChay(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var bestSellingProducts = db.ChiTietHdbs
                                        .GroupBy(ct => ct.MaSp)
                                        .Select(g => new
                                        {
                                            MaSp = g.Key,
                                            TotalQuantitySold = g.Sum(ct => ct.SoLuongBan)
                                        })
                                        .OrderByDescending(x => x.TotalQuantitySold)
                                        .Take(10)
                                        .Join(db.DanhMucSps, ct => ct.MaSp, sp => sp.MaSp, (ct, sp) => new
                                        {
                                            sp.AnhDaiDien,
                                            sp.MaSp,
                                            sp.TenSp,
                                            sp.GiaSanPham,
                                            TotalQuantitySold = ct.TotalQuantitySold
                                        })
                                        .ToList();

            return View(bestSellingProducts.ToPagedList(pageNumber, pageSize));
        }





    }



}
