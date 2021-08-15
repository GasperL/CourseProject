using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class Provider : BaseEntity
    {
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }

        public bool IsApproved { get; set; }
    }
}