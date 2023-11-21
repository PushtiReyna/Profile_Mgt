using System.ComponentModel.DataAnnotations;

namespace Profile_Mgt.ViewModel
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter Firstname"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid Firstname.")]
        public string Firstname { get; set; } = null!;

        [Required(ErrorMessage = "Please enter Middlename"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid Middlename.")]
        public string? Middlename { get; set; }

        [Required(ErrorMessage = "Please enter Lastname"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Invalid Lastname.")]
        public string Lastname { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = null!;

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter Date of Birth")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Please Enter Address"), MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "invalid address")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter pincode Number")]
        [RegularExpression(@"^[1-9]\d{5}$", ErrorMessage = "Please enter a valid 6-digit pin code.")]
        public int Pincode { get; set; }

        [Required(ErrorMessage = "Please Enter username"), MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "invalid username.")]
        public string Username { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password.")]
        [MinLength(3, ErrorMessage = "invalid password")]
        [MaxLength(5, ErrorMessage = "invalid password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please select Profile Image")]
        public IFormFile Image { get; set; }
    }
}
