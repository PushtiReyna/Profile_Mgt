using Microsoft.AspNetCore.Mvc;
using Profile_Mgt.Models;
using Profile_Mgt.ViewModel;

namespace Profile_Mgt.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UserProfileDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(UserProfileDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            var user = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());

            var categoryList = _db.CategoryMsts.Where(x => x.IsDelete == false && x.CategoryName == addCategoryViewModel.CategoryName.Trim()).ToList();

            if(categoryList.Count <= 0 && user != null)
            {
                CategoryMst categoryMst = new CategoryMst();

                string imgPath = UploadImage(addCategoryViewModel.Categoryimg);
                categoryMst.CategoryName = addCategoryViewModel.CategoryName;
                categoryMst.CategoryImage = imgPath;
                categoryMst.CreatedOn = DateTime.Now;
                categoryMst.CreatedBy = user.Id;
                categoryMst.IsActive = true;

                _db.CategoryMsts.Add(categoryMst);
                _db.SaveChanges();
                ViewBag.Message =  "category added successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "categoryname is already Exists.";
            }
            return View();
        }

        private string UploadImage(IFormFile Categoryimg)
        {
            string fileName = Categoryimg.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

            string relativePath = Path.Combine("Images", fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    Categoryimg.CopyTo(fileStream);
                }
            }
            return relativePath;
        }
    }
}
