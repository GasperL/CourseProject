using System;
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
        [Range(0, Double.MaxValue)]
        public double Discount { get; set; }

        [Range(0, 100)]
        [Required]
        public int BonusPoints { get; set; }
    }
}