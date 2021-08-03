using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class ProductCategory : BaseEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
    }
}