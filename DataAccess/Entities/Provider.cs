using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class Provider : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsApproved { get; set; }
    }
}