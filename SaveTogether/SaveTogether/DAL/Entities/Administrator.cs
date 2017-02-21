using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Administrator: AuthorizedPerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string SecondName { get; set; }

        public DateTime DateOfBitrth { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}