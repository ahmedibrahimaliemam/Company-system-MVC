using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DL.Models
{
	public class EmailModel
	{
		public int Id {  get; set; }
		[Required(ErrorMessage ="body is required")]
		public string Body { get; set; }
		[Required(ErrorMessage ="the field To is Required")]
		public string To { get; set; }
		[Required(ErrorMessage ="Subject is required")]
		public string Subject { get; set; }
	}
}
