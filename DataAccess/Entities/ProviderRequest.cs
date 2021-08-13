using System;

namespace DataAccess.Entities
{
    public class ProviderRequest : BaseEntity
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public Guid ProviderId { get; set; }

        public Provider Provider { get; set; }
        
        public ProviderRequestStatusEnum Status { get; set; }
    }
}