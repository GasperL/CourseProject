using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        
        public Guid CategoryId { get; set; }
        
        public Category Category { get; set; }

        public Guid ProductGroupId { get; set; }
        
        public ProductGroup ProductGroup { get; set; }
        
        public Guid ProviderId { get; set; }
        
        public Provider Provider { get; set; }

        public bool Accessibility { get; set; }

        [MaxLength(20)]
        public string ManufacturerName { get; set; }
        
        [MaxLength(20)]
        public string ProviderName { get; set; }
        
        [MaxLength(20)]
        public string ProductName { get; set; }
        
        public int Remainder { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }

    }
}