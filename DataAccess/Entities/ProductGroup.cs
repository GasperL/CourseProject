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
        [Range(0, 100)]
        public double Discount { get; set; }
    }
}