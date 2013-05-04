using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross;
using Cirrious.MvvmCross.Droid;
using Cirrious.MvvmCross.Platform;
using JabbrMobile.Common.Services;
using JabbR.Client.Models;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Messages;

namespace JabbrMobile.Common.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		MvxSubscriptionToken _mvxMsgTokenJabbrConnected;

		public HomeViewModel(ISettingsService settings, IJabbrService service, IMvxMessenger messenger) : base(settings, service, messenger)
		{
			_mvxMsgTokenJabbrConnected = Messenger.Subscribe<JabbrConnectedMessage> (msg => RaisePropertyChanged (() => Rooms));

			try
			{
				Service.AddClient(new JabbrMobile.Common.Models.Account()
				{
					AutoConnect = true,
					Url = "https://jabbr.net/",
					Username = "MoJabbr",
					Password = "mojabber"
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine (ex);
			}
		}

		public void SwitchVisibleRoomCommand(RoomInfo room)
		{
			this.VisibleRoom = room;
			RaisePropertyChanged (() => VisibleRoom);
		}

		public RoomInfo VisibleRoom { get; private set; }

		public List<RoomInfo> Rooms
		{
			get
			{
				var rooms = new List<RoomInfo> ();

				foreach (var c in Service.Clients)
				{
					foreach (var r in c.RoomsIn)
						rooms.Add(new RoomInfo () { Room = r, Jabbr = c });
				}

				return rooms;
			}
		}		 
	}

}