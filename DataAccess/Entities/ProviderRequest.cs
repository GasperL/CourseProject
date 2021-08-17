using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class ProviderRequest : BaseEntity
    {
        public new string Id { get; set; }
        
        public User User{ get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }
        
        public ProviderRequest Provider { get; set; }
        
        public string ProviderId { get; set; }
        
        [Required]
        public ProviderRequestStatus Status { get; set; }
    }
}