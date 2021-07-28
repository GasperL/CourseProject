using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class ProductOrder
    {
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        
        public ICollection<Product> Products { get; set; }

        public Guid UserDiscountId { get; set; }
        
        public UserDiscount UserDiscount { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
    }
}