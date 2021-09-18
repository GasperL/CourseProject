﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Common.ViewModels.MainEntityViewModels;

namespace Core.Common.ViewModels
{
    public class OrderItemViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserOrderId { get; set; }

        [MaxLength(20)] 
        [Required] 
        public string ProductName { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Price { get; set; }
        
        public double DiscountPercentage { get; set; } 

        public string CategoryName { get; set; } 

        public string ManufacturerName { get; set; } 

        public string ProviderName { get; set; }
    }
}