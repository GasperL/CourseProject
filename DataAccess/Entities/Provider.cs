using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Provider : BaseEntity
    {
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }

        public ProviderRequest ProviderRequest { get; set; }
        
        public string ProviderRequestId { get; set; }
    }
}