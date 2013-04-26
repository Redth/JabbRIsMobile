using System;

namespace JabbrMobile.Common.Models
{
	public class Account
	{
		public Account()
		{
			Id = Guid.NewGuid ().ToString ();
		}

		public string Id { get;set; }
		public string Url { get;set; }
		public string Username { get;set; }
		public string Password { get;set; }

		public bool AutoConnect { get;set; }


	}
}