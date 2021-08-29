using System.ComponentModel.DataAnnotations;

namespace Core.Common.CreateViewModels
{
    public class CreateProviderRequestViewModel
    {
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1200)]
        public string Description { get; set; }
        
        [Required]
        public string UserId { get; set; }
    }
}