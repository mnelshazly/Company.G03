﻿using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt is Required")]
        public DateTime CreateAt { get; set; }
    }
}
