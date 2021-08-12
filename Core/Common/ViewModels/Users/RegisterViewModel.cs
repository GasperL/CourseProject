using System.ComponentModel.DataAnnotations;

namespace Core.Common.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
       
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}