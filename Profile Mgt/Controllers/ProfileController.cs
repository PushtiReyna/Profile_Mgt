using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Profile_Mgt.Models;
using Profile_Mgt.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Profile_Mgt.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserProfileDbContext _db;
    
        public ProfileController(UserProfileDbContext db)
        {
            _db = db;
        }

        public IActionResult GetUserDetail()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var user = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());
                if (user != null)
                {
                    GetUserDetail getUserDetail = new GetUserDetail();
                    getUserDetail.Id = user.Id;
                    getUserDetail.Firstname = user.Firstname;
                    getUserDetail.Middlename = user.Middlename;
                    getUserDetail.Lastname = user.Lastname;
                    getUserDetail.Email = user.Email;
                    getUserDetail.Dob = user.Dob;
                    getUserDetail.Address = user.Address;
                    getUserDetail.Pincode = user.Pincode;
                    getUserDetail.Username = user.Username;
                    getUserDetail.ProfileImage = user.ProfileImage;
                    return View(getUserDetail);
                }
            }
            return RedirectToAction("Login","Login");
        }

        [HttpPost]
        public IActionResult DownloadPDF()
        {
            var user = _db.UserMsts.Where(x => x.Username == HttpContext.Session.GetString("UserSession").ToString() && HttpContext.Session.GetString("UserSession") != null).FirstOrDefault();
            if (user != null)
            {
                DownloadPDF downloadPDF = new DownloadPDF();
                downloadPDF.FullName = user.Firstname + " " + user.Middlename + " " + user.Lastname;
                downloadPDF.Email = user.Email;
                downloadPDF.Dob = user.Dob;
                downloadPDF.Address = user.Address;
                downloadPDF.Pincode = user.Pincode;


                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);

                if (System.IO.File.Exists("D:\\" + user.Username + ".pdf"))
                {
                    System.IO.File.Delete("D:\\" + user.Username + ".pdf");
                }
                FileStream FS = new FileStream("D:\\" + user.Username + ".pdf", FileMode.Create);

                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, FS);

                pdfDoc.Open();

                Paragraph header = new Paragraph("User Profile", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                header.SpacingBefore = 10f;
                header.SpacingAfter = 10f;
                header.IndentationLeft = 200f;
                pdfDoc.Add(header);

                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 300f;
                table.LockedWidth = true;
            
                PdfPCell cell = new PdfPCell();

                Paragraph Header = new Paragraph("FullName:", new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
                cell.AddElement(Header);
                cell.FixedHeight = 30f;
                cell.PaddingLeft = 6f;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell();
                Paragraph Data = new Paragraph(downloadPDF.FullName);
                cell.FixedHeight = 30f;
                cell.Border = 0;
                cell.PaddingLeft = 8f;
                cell.AddElement(Data);
                table.AddCell(cell);
  
                cell = new PdfPCell();
                Header = new Paragraph("Email:", new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
                cell.FixedHeight = 30f;
                cell.AddElement(Header);
                cell.PaddingLeft = 6f;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell();
                Data = new Paragraph(downloadPDF.Email);
                cell.FixedHeight = 30f;
                cell.Border = 0;
                cell.PaddingLeft = 8f;
                cell.AddElement(Data);
                table.AddCell(cell);

                cell = new PdfPCell();
                Header = new Paragraph("DOB:", new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
                cell.FixedHeight = 30f;
                cell.AddElement(Header);
                cell.PaddingLeft = 6f;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell();
                Data = new Paragraph(downloadPDF.Dob.ToString());
                cell.FixedHeight = 30f;
                cell.Border = 0;
                cell.PaddingLeft = 8f;
                cell.AddElement(Data);
                table.AddCell(cell);

                cell = new PdfPCell();
                Header = new Paragraph("Address:", new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
                cell.FixedHeight = 30f;
                cell.AddElement(Header);
                cell.PaddingLeft = 6f;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell();
                Data = new Paragraph(downloadPDF.Address);
                cell.FixedHeight = 30f;
                cell.Border = 0;
                cell.PaddingLeft = 8f;
                cell.AddElement(Data);
                table.AddCell(cell);

                cell = new PdfPCell();
                Header = new Paragraph("Pincode:", new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD));
                cell.FixedHeight = 30f;
                cell.AddElement(Header);
                cell.PaddingLeft = 6f;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell();
                Data = new Paragraph(downloadPDF.Pincode.ToString());
                cell.FixedHeight = 30f;
                cell.Border = 0;
                cell.PaddingLeft = 8f;
                cell.AddElement(Data);
                table.AddCell(cell);

                pdfDoc.Add(table);

                pdfWriter.CloseStream = false;
                pdfDoc.Close();

                FS.Close();
                TempData["message"] = "PDF download in D Drive";
            }
            return Redirect("GetUserDetail");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassowordViewModel changePassword)
        {
            var user = _db.UserMsts.Where(x => x.Username == HttpContext.Session.GetString("UserSession").ToString() && x.Password == changePassword.Password).FirstOrDefault();
            if (user != null && user.Password != changePassword.NewPassword)
            {
                user.Password = changePassword.NewPassword;
                user.UpdateBy = 1;
                user.UpdatedOn = DateTime.Now;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = "New password updated successfully";
                return RedirectToAction("GetUserDetail");
            }
            else if (user != null && user.Password == changePassword.NewPassword)
            {
                ViewBag.Message = "password is already exists";
            }
            else
            {
                ViewBag.Message = "password is not corect";
            }
            return View();
        }
    }
}
