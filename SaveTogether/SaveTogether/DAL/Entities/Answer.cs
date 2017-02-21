using System;
using SaveTogether.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Answer: Message
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

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required]
        public int? QuestionId { get; set; }

    }
}