using System;
using JabbrMobile.Common.Services;
using JabbR.Client.Models;

namespace JabbrMobile.Common.Models
{
	public class RoomInfo
	{
		public RoomInfo()
		{
		}

		public JabbrClientWrapper Jabbr { get;set; }
		public Room Room { get;set; }

		public string ServerDisplayName { 
			get {
				return Jabbr.Account.Url.Replace ("https:", "").Replace ("http:", "").Trim ('/');
			}
		}
	}
}