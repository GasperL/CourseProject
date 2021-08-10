using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public ICollection<OrderHistory> OrderHistories { get; set; }

        public Guid OrderHistoryId { get; set; }

        public int BonusPoints { get; set; }
    }
}