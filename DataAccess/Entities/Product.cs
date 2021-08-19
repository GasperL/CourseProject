using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public Guid CategoryId { get; set; }
        
        public Category Category { get; set; }

        [Required]
        public Guid ProductGroupId { get; set; }
        
        public ProductGroup ProductGroup { get; set; }
        
        [Required]
        public Guid ProviderId { get; set; }
        
        public Provider Provider { get; set; }
        
        [Required]
        public Guid ManufacturerId { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
        
        public bool IsAvailable { get; set; }

        [MaxLength(20)]
        [Required]
        public string ProductName { get; set; }
        
        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Price { get; set; }
        
        //public byte[] Image { get; set; }
    }
}