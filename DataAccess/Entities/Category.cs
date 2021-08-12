using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public Category SelectCategory { get; set; }
    }
}