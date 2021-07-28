using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class ProductGroup
    {
        public Guid Id { get; set; }
        
        [MaxLength(20)]
        public string Name { get; set; }
        
        public decimal Discount { get; set; }
    }
}