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
            var user = _db.UserMsts.Where(x => x.Username == loginViewModel.Username.Trim() && x.Password == loginViewModel.Password.Trim() && x.IsDelete == false).FirstOrDefault();
            if (user != null)
            {              
                HttpContext.Session.SetString("UserSession", user.Username);
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

        #region MyRegion
        //[HttpGet]
        //public IActionResult Registration()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Registration(RegistrationViewModel registrationViewModel)
        //{
        //    var userList = _db.UserMsts.Where(x => x.IsDelete == false && x.Username == registrationViewModel.Username.Trim()).ToList();
        //    var age = GetAge(registrationViewModel.Dob);
        //    if(age < 18)
        //    {
        //        ViewBag.UsernameMessage = "age is not 18+";
        //    }
        //    else if (userList.Count <= 0 && age >18)
        //    {
        //            UserMst userMst = new UserMst();

        //            string imgPath = UploadImage(registrationViewModel.Image);

        //            userMst.Firstname = registrationViewModel.Firstname.Trim();
        //            userMst.Middlename = registrationViewModel.Middlename.Trim();
        //            userMst.Lastname = registrationViewModel.Lastname.Trim();
        //            userMst.Email = registrationViewModel.Email.Trim();
        //            userMst.Dob = registrationViewModel.Dob;
        //            userMst.Address = registrationViewModel.Address.Trim();
        //            userMst.Pincode = registrationViewModel.Pincode;
        //            userMst.Username = registrationViewModel.Username.Trim();
        //            userMst.Password = registrationViewModel.Password.Trim();
        //            userMst.ProfileImage = imgPath;
        //            userMst.IsActive = true;
        //            userMst.CreatedBy = 1;
        //            userMst.CreatedOn = DateTime.Now;

        //            _db.UserMsts.Add(userMst);
        //            _db.SaveChanges();
        //            return RedirectToAction("Login");


        //    }
        //    else
        //    {
        //        ViewBag.UsernameMessage = "username is already Exists.";
        //    }
        //    return View();
        //}

        //private int GetAge(DateTime dob)
        //{
        //    DateTime today = DateTime.Today;
        //    int age = today.Year - dob.Year;

        //    if (dob > today.AddYears(-age))
        //        age--;
        //            return age;
        //}


        //public IActionResult GetUserDetail()
        //{

        //    var user = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());
        //    if (user != null)
        //    {
        //        GetUserDetail getUserDetail = new GetUserDetail();
        //        getUserDetail.Id = user.Id;
        //        getUserDetail.Firstname = user.Firstname;
        //        getUserDetail.Middlename = user.Middlename;
        //        getUserDetail.Lastname = user.Lastname;
        //        getUserDetail.Email = user.Email;
        //        getUserDetail.Dob = user.Dob;
        //        getUserDetail.Address = user.Address;
        //        getUserDetail.Pincode = user.Pincode;
        //        getUserDetail.Username = user.Username;
        //        getUserDetail.ProfileImage = user.ProfileImage;

        //        return View(getUserDetail);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}


        //[HttpGet]
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ChangePassword(ChangePassowordViewModel changePassword)
        //{

        //    var user = _db.UserMsts.Where(x => x.Username == HttpContext.Session.GetString("UserSession").ToString() && x.Password == changePassword.Password).FirstOrDefault();
        //    if (user != null && user.Password != changePassword.NewPassword)
        //    {
        //        user.Password = changePassword.NewPassword;
        //        user.UpdateBy = 1;
        //        user.UpdatedOn = DateTime.Now;
        //        _db.Entry(user).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        TempData["message"] = "New password updated successfully";
        //        return RedirectToAction("GetUserDetail");
        //    }
        //    else if (user != null && user.Password == changePassword.NewPassword)
        //    {
        //        ViewBag.Message = "password is already exists";
        //    }
        //    else
        //    {
        //        ViewBag.Message = "password is not corect";
        //    }
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult DownloadPDF()
        //{
        //    var user = _db.UserMsts.Where(x => x.IsDelete == false).FirstOrDefault();
        //    if (HttpContext.Session.GetString("UserSession") != null && HttpContext.Session.GetString("UserSession") == user.Username)
        //    {
        //        DownloadPDF downloadPDF = new DownloadPDF();

        //        downloadPDF.FullName = user.Firstname + user.Middlename + user.Lastname;
        //        downloadPDF.Email = user.Email;
        //        downloadPDF.Dob = user.Dob;
        //        downloadPDF.Address = user.Address;
        //        downloadPDF.Pincode = user.Pincode;


        //        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);

        //        if (System.IO.File.Exists("D:\\user.pdf"))
        //        {
        //            System.IO.File.Delete("D:\\user.pdf");
        //        }
        //        FileStream FS = new FileStream("D:\\user.pdf", FileMode.Create);

        //        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, FS);

        //        pdfDoc.Open();

        //        Paragraph header = new Paragraph("User Profile", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
        //        header.SpacingBefore = 10f;
        //        header.SpacingAfter = 10f;
        //        header.IndentationLeft = 200f;
        //        pdfDoc.Add(header);

        //        Paragraph para = new Paragraph();
        //        para = new Paragraph();
        //        para.Add("FullName");
        //        para.IndentationLeft = 50f;
        //        pdfDoc.Add(para);

        //        Paragraph para2 = new Paragraph(downloadPDF.FullName);
        //        para2.IndentationLeft = 200f;
        //        para2.SpacingBefore = 5f;
        //        pdfDoc.Add(para2);

        //        para = new Paragraph();
        //        para.Add("Email");
        //        para.IndentationLeft = 50f;
        //        pdfDoc.Add(para);

        //        para2 = new Paragraph(downloadPDF.Email);
        //        para2.IndentationLeft = 200f;
        //        pdfDoc.Add(para2);

        //        para = new Paragraph();
        //        para.Add("Dob");
        //        para.IndentationLeft = 50f;
        //        pdfDoc.Add(para);

        //        para2 = new Paragraph(downloadPDF.Dob.ToString());
        //        para2.IndentationLeft = 200f;
        //        pdfDoc.Add(para2);

        //        para = new Paragraph();
        //        para.Add("Address");
        //        para.IndentationLeft = 50f;
        //        pdfDoc.Add(para);

        //        para2 = new Paragraph(downloadPDF.Address);
        //        para2.IndentationLeft = 200f;
        //        pdfDoc.Add(para2);

        //        para = new Paragraph();
        //        para.Add("Pincode");
        //        para.IndentationLeft = 50f;
        //        pdfDoc.Add(para);

        //        para2 = new Paragraph(downloadPDF.Pincode.ToString());
        //        para2.IndentationLeft = 200f;
        //        pdfDoc.Add(para2);


        //        pdfWriter.CloseStream = false;
        //        pdfDoc.Close();

        //        FS.Close();
        //        TempData["message"] = "PDF download in D Drive";
        //    }

        //    return Redirect("GetUserDetail");
        //}

        //private string UploadImage(IFormFile Image)
        //{
        //    string fileName = Image.FileName;
        //    string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

        //    string filePath = Path.Combine("Images", fileName);
        //    if (fileName != null)
        //    {
        //        using (var fileStream = new FileStream(path, FileMode.Create))
        //        {
        //            Image.CopyTo(fileStream);
        //        }
        //    }
        //    return filePath;
        //}

        #endregion





    }
}





