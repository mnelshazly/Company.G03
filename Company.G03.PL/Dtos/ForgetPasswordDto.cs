﻿using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Dtos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
