using System.ComponentModel.DataAnnotations;

namespace Profile_Mgt.ViewModel
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Please enter CategoryName"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid CategoryName.")]
        public string CategoryName { get; set; } = null!;

        [Required(ErrorMessage = "Please select category Image")]
        public IFormFile Categoryimg { get; set; }
    }
}
