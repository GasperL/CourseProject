using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class UserOrder : BaseEntity
    {
        public ICollection<OrderItem> OrderItems { get; set; }

        public User User { get; set; }
        
        public string UserId { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal TotalPrice { get; set; }
    }
}