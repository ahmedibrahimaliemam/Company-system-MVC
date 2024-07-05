using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Email Is Required "),EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
