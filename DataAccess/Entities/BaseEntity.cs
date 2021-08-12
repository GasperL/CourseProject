using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}