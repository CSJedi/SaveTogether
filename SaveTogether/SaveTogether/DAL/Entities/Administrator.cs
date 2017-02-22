using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Administrator: AuthorizedPerson
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}