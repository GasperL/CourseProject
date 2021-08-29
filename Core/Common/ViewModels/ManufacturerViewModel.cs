using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Common.ViewModels
{
    public class ManufacturerViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}