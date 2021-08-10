using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class UserOrder : BaseEntity
    {
        public User User { get; set; }

        public string UserId { get; set; }
        
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}