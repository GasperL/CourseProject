using System;
using System.Collections.Generic;

namespace Core.ApplicationManagement.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        
        public string CategoryName { get; set; }

        public string ProductGroupName { get; set; }
        
        public double DiscountPercentages { get; set; }
        
        public string ManufacturerName { get; set; }
        
        public string ProviderName { get; set; }

        public bool IsAvailable { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
        
        public decimal DiscountPrice { get; set; }
        
        public ICollection<string> PhotoBase64 { get; set; }

        public string CoverPhotoBase64 { get; set; }
    }
}