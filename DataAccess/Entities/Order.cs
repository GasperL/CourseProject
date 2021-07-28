using System;

namespace DataAccess.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid ProductOrderId { get; set; }

        public ProductOrder ProductOrder { get; set; }

        public decimal TotalPrice { get; set; }
    }
}