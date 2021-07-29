using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Manufacturer : BaseEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
    }
}