using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class SignUpViewModel
    {
        [EmailAddress (ErrorMessage ="Invalid Format For Email")]
        public string Email { get; set; }
        [MaxLength(6)]
        public string Password { get; set; }
        [MaxLength(6)]
        [Compare("Password" , ErrorMessage ="Password Mismatch"),]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }



    }
}
