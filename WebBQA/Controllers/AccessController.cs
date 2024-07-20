using Microsoft.AspNetCore.Mvc;
using WebBQA.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
namespace WebBQA.Controllers
{
    public class AccessController : Controller  
    {
        QuanLyQaContext db = new QuanLyQaContext();

        public object Session { get; private set; }

        [HttpGet]

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("MaKhachHang") == null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(KhachHang user)
        {
            TempData["Message"] = "";
            if (HttpContext.Session.GetString("MaKhachHang") == null)
            {
                var u = db.KhachHangs.Where(x => x.MaKhachHang.Equals(user.MaKhachHang) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("MaKhachHang", u.MaKhachHang.ToString());


                    if (u.LoaiUserr == 1)
                        {
                            return RedirectToAction("index", "admin");
                        }
                    if (u.LoaiUserr != 1 && u.LoaiUserr!=2)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                   
                    else if(u.LoaiUserr == 2)
                    {
                        TempData["Message"] = "Tài khoản đã bị khoá ";
                        return View();
                    }


                }
            }
            TempData["Message"] = "Tài khoản hoặc mật khẩu không chính xác";

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("MaKhachHang");

            return RedirectToAction("Login", "Access");

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang _user)
        {
            TempData["Message"] = "";
            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(x => x.MaKhachHang == _user.MaKhachHang);
                if (check == null)
                {
                    //  _user.Password = GetMD5(_user.Password);

                    db.KhachHangs.Add(_user);
                    db.SaveChanges();
                    TempData["Message"] = "Đăng ký thành công";
                    return RedirectToAction("Login");



                }
                else
                {
                    TempData["Message"] = "Tài khoản đã tồn tại !";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5
        /*
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        */


    }
}