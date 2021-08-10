﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class Provider : User
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public Guid RoleId { get; set; }
    }
}