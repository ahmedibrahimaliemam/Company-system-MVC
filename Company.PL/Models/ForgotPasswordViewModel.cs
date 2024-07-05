using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required "), EmailAddress]
		public string Email { get; set; }
	}
}
