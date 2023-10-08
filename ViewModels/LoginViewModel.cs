using System.ComponentModel.DataAnnotations;

namespace FightClubWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "EmailAddress address is required")]
        [Display(Name = "EmailAddress Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
