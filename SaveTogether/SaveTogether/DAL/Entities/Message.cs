using System;
using SaveTogether.Interfaces;
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
        public IdentityUser Person { get; set; }

        [Required]
        public int? PersonId { get; set; }
    }
}