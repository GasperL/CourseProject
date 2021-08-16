using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public int BonusPoints { get; set; }
        
        public ICollection<ProviderRequest> ProviderRequests { get; set; }

        public Guid ProviderRequestId { get; set; }
    }
}