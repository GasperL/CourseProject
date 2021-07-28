using System;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}