using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Entities;

namespace Core.Common.ViewModels
{
    public class OrderItemViewModel
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
    }
}