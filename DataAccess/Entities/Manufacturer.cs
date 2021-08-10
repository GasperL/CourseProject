using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Manufacturer : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public Manufacturer SelectManufacturer { get; set; }
    }
}