using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is Required !!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required !!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$",
                          ErrorMessage = "Passwords must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, and one non-alphanumeric character.")]
        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm password does not match the password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
