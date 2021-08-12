using System;

namespace DataAccess.Entities
{
    public class UserOrderHistory: BaseEntity
    {
        public UserOrder UserOrder { get; set; }

        public Guid UserOrderId { get; set; }

        public bool IsOrderSuccessful { get; set; }

        public DateTime OrderDate { get; set; }
    }
}