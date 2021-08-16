using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class ProviderRequest : BaseEntity
    {
        public User User{ get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }
        
        [Required]
        public ProviderRequestStatus Status { get; set; }
    }
}