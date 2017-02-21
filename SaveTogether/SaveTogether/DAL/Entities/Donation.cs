using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SaveTogether.Interfaces;

namespace SaveTogether.DAL.Entities
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public DateTime OperationDateTime { get; set; }

        [ForeignKey("RegionId")]
        public Region Region { get; set; }

        [Required]
        public int? RegionId { get; set; }

        [ForeignKey("PersonId")]
        public IPerson Person { get; set; }

        [Required]
        public int? PersonId { get; set; }
    }
}