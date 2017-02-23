using System;
using System.ComponentModel.DataAnnotations;

namespace SaveTogether.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public string SecondName { get; set; }

        public DateTime DateOfBitrth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}