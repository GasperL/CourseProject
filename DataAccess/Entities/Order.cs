using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Order : BaseEntity
    {
        public Guid ProductOrderId { get; set; }

        public ICollection<ProductOrder> ProductOrder { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }
    }
}