using System;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;

namespace JabbrMobile.Common.ViewModels
{
	public class RoomViewModel : BaseViewModel
	{
		public RoomViewModel() : base()
		{
		}

		public JabbrConnection Jabbr { get;set; }
		public Room Room { get;set; }

		public string ServerDisplayName { 
			get {
				return Jabbr.Account.Url.Replace ("https:", "").Replace ("http:", "").Trim ('/');
			}
		}
	}

}