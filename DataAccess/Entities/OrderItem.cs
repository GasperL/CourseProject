using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    
    public class OrderItem : BaseEntity
    {
        public Guid UserOrderId { get; set; }
        
        public UserOrder UserOrder { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Product Product { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        
        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Price { get; set; }
    
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
    }
}