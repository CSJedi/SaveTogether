using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Answer: Message
    {
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? QuestionId { get; set; }

    }
}