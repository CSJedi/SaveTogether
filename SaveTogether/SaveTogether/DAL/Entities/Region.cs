using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveTogether.DAL.Entities
{
    public class Region
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Required]
        public int? CountryId { get; set; }

        [Required]
        public int Population { get; set; }
    }
}