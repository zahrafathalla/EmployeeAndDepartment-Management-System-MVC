using NuGet.Protocol.Plugins;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);

			client.EnableSsl= true;
			client.Credentials = new NetworkCredential("zahragamal546@gmail.com", "xssjmytwyabkewju");

			client.Send("zahragamal546@gmail.com", email.To, email.Title, email.Body);
		}

	}

}
