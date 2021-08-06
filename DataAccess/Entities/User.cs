using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserOrder> Orders { get; set; }

        public Guid UserOrderId { get; set; }

        public int BonusPoints { get; set; }
    }
}