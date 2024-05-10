using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class ResetPasswordViewModel
    {
        public string Password { get; set; }
        [MaxLength(6)]
        [Compare("Password", ErrorMessage = "Password Mismatch"),]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


    }
}
