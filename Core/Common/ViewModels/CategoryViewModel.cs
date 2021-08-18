using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Common.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }
    }
}