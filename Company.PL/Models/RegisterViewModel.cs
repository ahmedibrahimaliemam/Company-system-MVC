using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="FirstName is required")]
		public string FName { get; set; }
		[Required(ErrorMessage ="LastName is required")]
		public string LName { get; set; }
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage ="password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage ="Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="Must matches the Password")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage ="the Agree status is Required")]
		public bool Agree { get; set; }

	}
}
