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
		MvxSubscriptionToken _mvxMsgTokenJoinedRoom;

		public HomeViewModel() : base()
		{
			_mvxMsgTokenJabbrConnected = Messenger.Subscribe<JabbrConnectedMessage> (msg => RaisePropertyChanged (() => Rooms));
			_mvxMsgTokenJoinedRoom = Messenger.Subscribe<JabbrJoinedRoomMessage> (msg => RaisePropertyChanged(() => Rooms));


			Settings.Accounts.Add (new JabbrMobile.Common.Models.Account() {
				AutoConnect = true,
				Url = "https://jabbr.net/",
				Username = "MoJabbr",
				Password = "mojabber"
			});

		}

		public ICommand ShowAccountsCommand
		{
			get { return new MvxCommand (() => ShowViewModel<AccountsViewModel>()); }
		}

		public ICommand ShowSettingsCommand
		{
			get { return new MvxCommand (() => ShowViewModel<SettingsViewModel>()); }
		}

		public ICommand ShowRoomListCommand
		{
			get { return new MvxCommand (() => ShowViewModel<RoomListViewModel>()); }
		}

		public ICommand LeaveCurrentRoomCommand
		{
			get
			{
				return new MvxCommand ( async () => {

					var room = this.CurrentRoom;

					await room.Connection.LeaveRoom(room.Room.Name);

					CurrentRoom = null;
					RaisePropertyChanged (() => CurrentRoom);

					RaisePropertyChanged(() => Rooms);
				});
			}
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