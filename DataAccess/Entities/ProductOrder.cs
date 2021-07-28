using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class ProductOrder
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Product Product { get; set; }

        public Guid UserDiscountId { get; set; }
        
        public UserDiscount UserDiscount { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
    }
}