using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class ProductGroup : BaseEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
    }
}