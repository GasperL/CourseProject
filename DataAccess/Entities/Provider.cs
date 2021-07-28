using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Provider
    {
        public Guid Id { get; set; }
        
        public Guid ManufacturerId { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
       
        [MaxLength(20)]
        public string Name { get; set; }
    }
}