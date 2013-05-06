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
		public event Action<RoomViewModel> OnSwitchRoom;

		MvxSubscriptionToken _mvxMsgTokenJabbrConnected;

		public HomeViewModel() : base()
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

		public void SwitchRoomCommand(RoomViewModel room)
		{
			Console.WriteLine ("Switch Room: " + room.Room.Name);

			this.CurrentRoom = room;
			RaisePropertyChanged (() => CurrentRoom);
		}

		public RoomViewModel CurrentRoom { get; private set; }

		public List<RoomViewModel> Rooms
		{
			get
			{
				var rooms = new List<RoomViewModel> ();

				foreach (var c in Service.Connections)
				{
					foreach (var r in c.RoomsIn)
						rooms.Add(new RoomViewModel () { Room = r, Jabbr = c });
				}

				return rooms;
			}
		}		 
	}

}