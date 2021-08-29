using System.ComponentModel.DataAnnotations;

namespace Core.Common.ViewModels.Users
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
        
        public string BonusPoints { get; set; }
    }
}
