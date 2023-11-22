using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Profile_Mgt.Models;
using Profile_Mgt.ViewModel;

namespace Profile_Mgt.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly UserProfileDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SubCategoryController(UserProfileDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult AddSubCategory()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());

                var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false && u.CreatedBy == userDetail.Id).ToList();
                ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult AddSubCategory(AddSubCategoryViewModel addSubCategoryViewModel)
        {
            var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());

            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false && u.CreatedBy == userDetail.Id).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            var subCategoryList = _db.SubcategoryMsts.Where(x => x.IsDelete == false && x.SubcategoryName == addSubCategoryViewModel.SubcategoryName.Trim()).ToList();

            if (subCategoryList.Count <= 0 && userDetail != null)
            {
                SubcategoryMst subcategoryMst = new SubcategoryMst();

                string imgPath = UploadImage(addSubCategoryViewModel.Subcategoryimg);

                subcategoryMst.SubcategoryName = addSubCategoryViewModel.SubcategoryName;
                subcategoryMst.CategoryId = addSubCategoryViewModel.CategoryId;
                subcategoryMst.SubcategoryImage = imgPath;
                subcategoryMst.CreatedOn = DateTime.Now;
                subcategoryMst.CreatedBy = userDetail.Id;
                subcategoryMst.IsActive = true;

                _db.SubcategoryMsts.Add(subcategoryMst);
                _db.SaveChanges();
                ViewBag.Message = "category added successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "categoryname is already Exists.";
            }
            return View();
        }

        private string UploadImage(IFormFile Subcategoryimg)
        {
            string fileName = Subcategoryimg.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

            string relativePath = Path.Combine("Images", fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    Subcategoryimg.CopyTo(fileStream);
                }
            }
            return relativePath;
        }
    }
}
