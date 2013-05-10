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

		public string ServerDisplayName 
		{
			get { return Url.Replace ("http://", "").Replace ("https://", "").Trim('/'); }
		}


		public override bool Equals (object obj)
		{
			if (!(obj is Account))
				return false;

			var a = obj as Account;

			if (a == null)
				return false;

			return a.Id.Equals (this.Id, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}