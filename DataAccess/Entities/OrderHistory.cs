using System;

namespace DataAccess.Entities
{
    public class OrderHistory : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }
        
        public Guid UserOrderId { get; set; }
        
        public UserOrder UserOrder { get; set; }

        public Guid ProviderId { get; set; }

        public Provider Provider { get; set; }

        public bool IsOrderSuccessful { get; set; }
    }
}