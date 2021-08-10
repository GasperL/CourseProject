using System.ComponentModel.DataAnnotations;

namespace Core.Common.CreateViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public enum UserType
    {
        User,
        Provider,
    }
}
