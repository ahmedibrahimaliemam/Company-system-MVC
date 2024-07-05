using Company.DL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helper
{
	public class EmailHelper
	{
		public static void SendEmail(EmailModel email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("ahmedibrahimaliemam@gmail.com", "iboruxfpzemcmwlf");//app password
			client.Send("ahmedibrahimaliemam@gmail.com",email.To,email.Subject,email.Body);

		}
	}
}
