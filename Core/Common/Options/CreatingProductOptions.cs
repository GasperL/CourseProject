using System;
using DataAccess.Entities;

namespace Core.Common.Options
{
    public class CreatingProductOptions
    {
        public Guid Id { get; set; }

        public Category Category { get; set; }  
        
        public ProductGroup Group { get; set; }  
        
        public Manufacturer Manufacturer { get; set; }  
       
        public decimal Price { get; set; }  
        
        public string ProductName { get; set; }  
        
        public int Amount { get; set; }  
    }
}