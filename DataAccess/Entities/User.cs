using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserOrder> UserOrders { get; set; }

        public int BonusPoints { get; set; }
    }
}