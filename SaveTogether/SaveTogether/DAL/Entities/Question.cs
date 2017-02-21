using System;
using SaveTogether.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Question: Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime MessageDateTime { get; set; }

        [ForeignKey("PersonId")]
        public IPerson Person { get; set; }

        [Required]
        public int? PersonId { get; set; }
    }
}