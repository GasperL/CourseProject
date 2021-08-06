using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class ProductGroup : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Discount { get; set; }

        public int BonusPoints { get; set; }
    }
}