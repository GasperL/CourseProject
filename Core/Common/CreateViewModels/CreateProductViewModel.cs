using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Common.CreateViewModels
{
    public class CreateProductViewModel
    {
        public SelectListItem[] SelectCategory { get; set; }
        
        public SelectListItem[] SelectProductGroups { get; set; }
        
        public SelectListItem[] SelectManufacturer { get; set; }
        
        public Guid CategoryId { get; set; }  
        
        public Guid ProductGroupId { get; set; }  
        
        public Guid ManufacturerId { get; set; }

        public Guid ProviderId { get; set; }
       
        [Required]
        public decimal Price { get; set; }  
        
        [Required]
        public string ProductName { get; set; }  
        
        [Required]
        public int Amount { get; set; }  
    }
}