using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class PersonalDiscount
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
    }
}