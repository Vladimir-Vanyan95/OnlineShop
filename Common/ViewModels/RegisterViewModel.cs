using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is wrong")]
        public string ConfirmPassword { get; set; }
    }
}
