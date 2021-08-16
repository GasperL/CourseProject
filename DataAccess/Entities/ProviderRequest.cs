using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class ProviderRequest : BaseEntity
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public Guid ProviderId { get; set; }

        public Provider Provider { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }
        
        public ProviderRequestStatusEnum Status { get; set; }
    }
}