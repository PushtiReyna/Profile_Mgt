using System.ComponentModel.DataAnnotations;

namespace Profile_Mgt.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter username"), MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "invalid username.")]
        public string Username { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password.")]
        [MinLength(3, ErrorMessage = "invalid password")]
        [MaxLength(5, ErrorMessage = "invalid password")]
        public string Password { get; set; } = null!;
    }
}
