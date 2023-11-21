using System.ComponentModel.DataAnnotations;

namespace Profile_Mgt.ViewModel
{
    public class AddSubCategoryViewModel
    {
        [Required(ErrorMessage = "Please enter SubcategoryName"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid SubcategoryName.")]
        public string SubcategoryName { get; set; } = null!;

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please select subcategory Image")]
        public IFormFile Subcategoryimg { get; set; }
    }
}
