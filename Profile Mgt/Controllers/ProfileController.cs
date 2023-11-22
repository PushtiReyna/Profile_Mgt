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
                var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString());
                if (userDetail != null)
                {
                    GetUserDetail getUserDetail = new GetUserDetail();
                    getUserDetail.Id = userDetail.Id;
                    getUserDetail.Firstname = userDetail.Firstname;
                    getUserDetail.Middlename = userDetail.Middlename;
                    getUserDetail.Lastname = userDetail.Lastname;
                    getUserDetail.Email = userDetail.Email;
                    getUserDetail.Dob = userDetail.Dob;
                    getUserDetail.Address = userDetail.Address;
                    getUserDetail.Pincode = userDetail.Pincode;
                    getUserDetail.Username = userDetail.Username;
                    getUserDetail.ProfileImage = userDetail.ProfileImage;
                    return View(getUserDetail);
                }
            }
            return RedirectToAction("Login","Login");
        }

        [HttpPost]
        public IActionResult DownloadPDF()
        {
            var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString() && HttpContext.Session.GetString("UserSession") != null);
            if (userDetail != null)
            {
                DownloadPDF downloadPDF = new DownloadPDF();
                downloadPDF.FullName = userDetail.Firstname + " " + userDetail.Middlename + " " + userDetail.Lastname;
                downloadPDF.Email = userDetail.Email;
                downloadPDF.Dob = userDetail.Dob;
                downloadPDF.Address = userDetail.Address;
                downloadPDF.Pincode = userDetail.Pincode;


                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);

                if (System.IO.File.Exists("D:\\" + userDetail.Username + ".pdf"))
                {
                    System.IO.File.Delete("D:\\" + userDetail.Username + ".pdf");
                }
                FileStream FS = new FileStream("D:\\" + userDetail.Username + ".pdf", FileMode.Create);

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
            var userDetail = _db.UserMsts.FirstOrDefault(x => x.Username == HttpContext.Session.GetString("UserSession").ToString() && changePassword.Password != null && changePassword.NewPassword != null && changePassword.ConfirmPassword != null && x.Password == changePassword.Password);

            if (userDetail != null && userDetail.Password != changePassword.NewPassword)
            {
                userDetail.Password = changePassword.NewPassword;
                userDetail.UpdateBy = 1;
                userDetail.UpdatedOn = DateTime.Now;
                _db.Entry(userDetail).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["message"] = "New password updated successfully";
                return RedirectToAction("GetUserDetail");
            }
            else if (userDetail != null && userDetail.Password == changePassword.NewPassword)
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
