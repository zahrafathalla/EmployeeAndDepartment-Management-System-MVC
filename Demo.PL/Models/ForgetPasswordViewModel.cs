﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Format For Email")]
		public string Email { get; set; }
	}
}
