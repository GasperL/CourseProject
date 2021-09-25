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

        public int Amount { get; set; }
    }
}