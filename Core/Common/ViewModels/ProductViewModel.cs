using System;
using System.Collections.Generic;
using DataAccess.Entities;

namespace Core.Common.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        
        public Category Category { get; set; }

        public ProductGroup ProductGroup { get; set; }
        
        public Manufacturer Manufacturer { get; set; }

        public Provider Provider { get; set; }

        public bool IsAvailable { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
        
        public decimal DiscountPrice { get; set; }
        
        public ICollection<string> PhotoBase64 { get; set; }
    }
}