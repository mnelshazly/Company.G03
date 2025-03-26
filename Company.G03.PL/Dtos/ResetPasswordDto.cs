using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Dtos
{
    public class ResetPasswordDto
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$",
                  ErrorMessage = "Passwords must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, and one non-alphanumeric character.")]
        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm password does not match the password")]
        public string ConfirmPassword { get; set; }
    }
}
