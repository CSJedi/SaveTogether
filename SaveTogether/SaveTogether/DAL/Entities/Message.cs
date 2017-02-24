using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaveTogether.DAL.Entities
{
    public abstract class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime MessageDateTime { get; set; }

        [ForeignKey("PersonId")]
        public virtual IdentityUser Person { get; set; }

        [Required]
        public string PersonId { get; set; }
    }
}