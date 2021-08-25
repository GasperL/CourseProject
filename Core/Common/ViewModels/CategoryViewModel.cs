using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Common.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}