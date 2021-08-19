using System.ComponentModel.DataAnnotations;

namespace Core.Common.CreateViewModels
{
    public class CreateProductGroupViewModel
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public double Discount { get; set; }

        [Range(0, 100)]
        [Required]
        public int BonusPoints { get; set; }
    }
}