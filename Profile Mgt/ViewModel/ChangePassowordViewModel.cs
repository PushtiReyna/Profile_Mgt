using System.ComponentModel.DataAnnotations;

namespace Profile_Mgt.ViewModel
{
    public class ChangePassowordViewModel
    {

        public string Username { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password.")]
        [MinLength(3, ErrorMessage = "invalid password")]
        [MaxLength(5, ErrorMessage = "invalid password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter New Password.")]
        [MinLength(3, ErrorMessage = "invalid New password")]
        [MaxLength(5, ErrorMessage = "invalid New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }

    }
}
