using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
	public class LoginViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Format For Email")]
		public string Email { get; set; }
		[MaxLength(6)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
