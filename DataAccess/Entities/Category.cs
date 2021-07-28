using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        
        [MaxLength(20)]
        public string Name { get; set; }
    }
}