using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Common.Attributes;
using Core.Common.Constants;
using Microsoft.AspNetCore.Http;


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
        [Range(0, 100000)]
        public decimal Price { get; set; }  
        
        [Required]
        public string ProductName { get; set; }  
        
        [Required]
        [Range(0, 100000)]
        public int Amount { get; set; }  
        
        [Required]
        [AllowedExtensions(new []{".jpg", ".png"})]
        [MaxFileSize(AttributeConstants.MaxFileSize)]
        public IFormFile Photo { get; set; }

        public string PhotoPath { get; set; }
    }
}