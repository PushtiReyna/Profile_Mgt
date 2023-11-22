using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Profile_Mgt.Models;
using Profile_Mgt.ViewModel;


namespace Profile_Mgt.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserProfileDbContext _db;
        
        public LoginController(UserProfileDbContext db)
        {
            _db = db;
        }

      
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("GetUserDetail","Profile");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == loginViewModel.Username.Trim() && loginViewModel.Password != null && x.Password == loginViewModel.Password.Trim() && x.IsDelete == false );
            if (userDetail != null)
            {              
                HttpContext.Session.SetString("UserSession", userDetail.Username);
                return RedirectToAction("GetUserDetail","Profile");
            }
            else
            {
                ViewBag.Message = "USERNAME OR PASSWORD NOT CORRECT";
                ModelState.Clear();
                return View();
            }
        }
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
            }
            return RedirectToAction("Login");
        }
    }
}





