using Microsoft.AspNetCore.Mvc;
using Profile_Mgt.Models;
using Profile_Mgt.ViewModel;

namespace Profile_Mgt.Controllers
{
    public class UploadController : Controller
    {
        private readonly UserProfileDbContext _db;
        public UploadController(UserProfileDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetImages()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());

                var ImagesList = (from u in _db.SubcategoryMsts.Where(u => u.IsDelete == false && u.CreatedBy == userDetail.Id)
                                  join j in _db.CategoryMsts.Where(u => u.IsDelete == false && u.CreatedBy == userDetail.Id)
                                  on u.CategoryId equals j.CategoryId
                                  select new GetImagesViewModel
                                  {
                                      SubcategoryName = u.SubcategoryName,
                                      SubcategoryImage = u.SubcategoryImage,
                                      CategoryName = j.CategoryName,
                                      CategoryImage = j.CategoryImage
                                  }).ToList();
                return View(ImagesList);
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
