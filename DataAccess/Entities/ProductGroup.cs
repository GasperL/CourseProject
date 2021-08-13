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
        public double Discount { get; set; }

        public int BonusPoints { get; set; }

        [NotMapped]
        public ProductGroup SelectProductGroup { get; set; }
    }
}