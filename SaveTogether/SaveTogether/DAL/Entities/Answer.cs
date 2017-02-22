using System;
using SaveTogether.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Answer: Message
    {
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required]
        public int? QuestionId { get; set; }

    }
}