using System.ComponentModel.DataAnnotations;
namespace Company.PL.Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "new password is required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Must matches the Password")]
		public string ConfirmPassword { get; set; }
	}
}
