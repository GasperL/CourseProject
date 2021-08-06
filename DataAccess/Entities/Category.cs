using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}