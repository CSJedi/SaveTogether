using System;
using System.ComponentModel.DataAnnotations;


namespace SaveTogether.Models
{
    public class EditModel
    {
        //[Required]
        public string Email { get; set; }

        //[Required]
        public string UserName { get; set; }

        public string SecondName { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}