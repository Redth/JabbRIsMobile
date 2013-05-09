using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cirrious.CrossCore;
using System.Threading.Tasks;

namespace JabbrMobile.Common.ViewModels
{
	public class RoomListViewModel : BaseViewModel
	{
		public RoomListViewModel() : base()
		{
			Rooms = new ObservableCollection<RoomListItemViewModel> ();
		}

		protected override void InitFromBundle (IMvxBundle parameters)
		{
			base.InitFromBundle (parameters);

			LoadRooms ();
		}

		public ObservableCollection<RoomListItemViewModel> Rooms { get; set; }

		public ICommand JoinRoomCommand
		{
			get
			{
				return new MvxCommand<RoomListItemViewModel> ( async room => {
					await room.Jabbr.Client.JoinRoom(room.Room.Name);
				});
			}
		}

		async void LoadRooms()
		{
			foreach (var c in Service.Connections)
			{
				try
				{
					var rooms = await c.Client.GetRooms();

					lock(Rooms)
					{
						foreach (var r in rooms)
							Rooms.Add(new RoomListItemViewModel(c, r));
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(c.Account.Url + " -> GetRooms Failed: " + ex);
				}

			}
		}
	}
}