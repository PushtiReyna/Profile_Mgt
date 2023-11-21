using Microsoft.AspNetCore.Mvc;
using Profile_Mgt.Models;
using Profile_Mgt.ViewModel;

namespace Profile_Mgt.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserProfileDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RegistrationController(UserProfileDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            var userList = _db.UserMsts.Where(x => x.IsDelete == false && x.Username == registrationViewModel.Username.Trim()).ToList();
            var age = GetAge(registrationViewModel.Dob);
            if (age < 18)
            {
                ViewBag.AgeMessage = "Age should not be less than 18";
            }
            else if (userList.Count <= 0 && age > 18)
            {
                UserMst userMst = new UserMst();

                string imgPath = UploadImage(registrationViewModel.Image);

                userMst.Firstname = registrationViewModel.Firstname.Trim();
                userMst.Middlename = registrationViewModel.Middlename.Trim();
                userMst.Lastname = registrationViewModel.Lastname.Trim();
                userMst.Email = registrationViewModel.Email.Trim();
                userMst.Dob = registrationViewModel.Dob;
                userMst.Address = registrationViewModel.Address.Trim();
                userMst.Pincode = registrationViewModel.Pincode;
                userMst.Username = registrationViewModel.Username.Trim();
                userMst.Password = registrationViewModel.Password.Trim();
                userMst.ProfileImage = imgPath;
                userMst.IsActive = true;
                userMst.CreatedBy = 1;
                userMst.CreatedOn = DateTime.Now;

                _db.UserMsts.Add(userMst);
                _db.SaveChanges();
                TempData["Success"] = "Registration successfully!";
                return RedirectToAction("Login","Login");
            }
            else
            {
                ViewBag.UsernameMessage = "username is already Exists.";
            }
            return View();
        }

        private int GetAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;

            if (dob > today.AddYears(-age))
                age--;
            return age;
        }

        private string UploadImage(IFormFile Image)
        {
            string fileName = Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

            string relativePath = Path.Combine("Images", fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return relativePath;
        }
    }
}
