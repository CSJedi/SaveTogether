using System;
using SaveTogether.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public abstract class AuthorizedPerson: IPerson
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
    }
}