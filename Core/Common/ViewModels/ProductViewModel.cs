using System;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Common.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public SelectList SelectCategory { get; set; }

        public Category Category { get; set; }

        public SelectList SelectProductGroups { get; set; }

        public ProductGroup ProductGroup { get; set; }
        
        public SelectList SelectManufacturer { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public Guid ProductGroupId { get; set; }

        public Guid ProviderId { get; set; }

        public Provider Provider { get; set; }

        public Guid ManufacturerId { get; set; }

        public bool IsAvailable { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }

        [Required] public decimal Price { get; set; }
    }
}