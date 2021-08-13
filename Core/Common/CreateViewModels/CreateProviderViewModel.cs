using System.ComponentModel.DataAnnotations;

namespace Core.Common.CreateViewModels
{
    public class CreateProviderViewModel
    {
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        
        public string UserId { get; set; }
    }
}