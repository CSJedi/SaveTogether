using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using Worldpay.Sdk.Enums;

namespace SaveTogether.DAL.Entities
{
    public class Donation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int? Sum { get; set; }

        public DateTime? OperationDateTime { get; set; }

        [ForeignKey("RegionId")]
        public Region Region { get; set; }

        public Guid? RegionId { get; set; }

        [ForeignKey("PersonId")]
        public virtual IdentityUser Person { get; set; }

        public string PersonId { get; set; }

        public string Token { get; set; }

        public CurrencyCode CurrencyCode { get; set; }
    }
}