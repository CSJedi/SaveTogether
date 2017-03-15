using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using Worldpay.Sdk.Enums;

namespace SaveTogether.DAL.Entities
{
    public class Donation
    {
        //TODO: need change/add some fields for WorldPayService
        [Key]
        public int Id { get; set; }

        [Required]
        public int? Sum { get; set; }

      //  [Required]
        public DateTime OperationDateTime { get; set; }

        [ForeignKey("RegionId")]
        public Region Region { get; set; }

        [Required]
        public int? RegionId { get; set; }

        [ForeignKey("PersonId")]
        public virtual IdentityUser Person { get; set; }

       // [Required]
        public string PersonId { get; set; }

        public string Token { get; set; }
        public CurrencyCode CurrencyCode { get; set; }
    }
}