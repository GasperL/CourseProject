using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class Provider : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}