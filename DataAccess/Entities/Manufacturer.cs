using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Manufacturer : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}