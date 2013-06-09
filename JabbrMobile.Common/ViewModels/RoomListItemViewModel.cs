using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;
using System.Collections.ObjectModel;
using JabbrMobile.Common.Messages;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using System.Windows.Input;

namespace JabbrMobile.Common.ViewModels
{
	public class RoomListItemViewModel : BaseViewModel
	{
		public RoomListItemViewModel(JabbrConnection jabbr, Room room)
		{
			Jabbr = jabbr;
			Room = room;
		}

		public JabbrConnection Jabbr { get; private set; }
		public Room Room { get;set; }
		public HomeViewModel Home { get;set; }

		public bool IsCurrent 
		{ 
			get
			{ 
				if (Home == null || Home.CurrentRoom == null)
					return false;

				return Home.CurrentRoom.Room.Name.Equals (Room.Name, StringComparison.OrdinalIgnoreCase);
			}
		}

		public string ServerDisplayName { 
			get {
				return Jabbr.Account.Username + " @ " + Jabbr.Account.Url.Replace ("https:", "").Replace ("http:", "").Trim ('/');
			}
		} 


	}

}