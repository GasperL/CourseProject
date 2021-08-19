using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities
{
    public class ProviderRequest : BaseEntity
    {
        public new string Id { get; set; }
        
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }

        [Required]
        public ProviderRequestStatus Status { get; set; }
    }
}