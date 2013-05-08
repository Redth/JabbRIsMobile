using System;
using System.Linq;
using Cirrious.CrossCore.Exceptions;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross;
using Cirrious.MvvmCross.Platform;
using JabbrMobile.Common.Services;
using JabbR.Client.Models;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Messages;
using System.Windows.Input;
using System.Reflection;
using Cirrious.CrossCore;

namespace JabbrMobile.Common.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
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
				Mvx.Error(ex.ToLongString());
			}
		}

		public ICommand ShowAccountsCommand
		{
			get { return new MvxCommand (() => ShowViewModel<AccountsViewModel>()); }
		}

		public ICommand ShowSettingsCommand
		{
			get { return new MvxCommand (() => ShowViewModel<SettingsViewModel>()); }
		}

		public ICommand SwitchRoomCommand
		{
			get { 
				return new MvxCommand<RoomViewModel> (room => {
					Mvx.Trace("Switch Room: " + room.Room.Name);

					this.CurrentRoom = room;
					RaisePropertyChanged (() => CurrentRoom);
				});
			}
		}

		/*public void SwitchRoom2Command(RoomViewModel room)
		{
			Console.WriteLine ("Switch Room: " + room.Room.Name);

			this.CurrentRoom = room;
			RaisePropertyChanged (() => CurrentRoom);
		}*/

		public RoomViewModel CurrentRoom { get; private set; }

		public List<RoomViewModel> Rooms
		{
			get
			{
				var rooms = new List<RoomViewModel> ();

				foreach (var c in Service.Connections)
				{
					foreach (var r in c.RoomsIn)
						rooms.Add(new RoomViewModel (c, r));
				}

				return rooms;
			}
		}		 
	}

}