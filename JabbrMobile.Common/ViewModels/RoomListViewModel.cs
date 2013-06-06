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
using JabbrMobile.Common.Messages;

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
		}

		public bool IsLoading { get; set; }

		public ObservableCollection<RoomListItemViewModel> Rooms { get; set; }

		public ICommand JoinRoomCommand
		{
			get
			{
				return new MvxCommand<RoomListItemViewModel> ( async room => {

					try 
					{ 
						await room.Jabbr.Client.JoinRoom(room.Room.Name); 

						room.Jabbr.RoomsIn.Add(room.Room);
					}
					catch { }


					Close(this);
				});
			}
		}

		public async Task LoadRooms()
		{
			IsLoading = true;
			RaisePropertyChanged (() => IsLoading);

			foreach (var c in Service.Connections)
			{
				try
				{
					var rooms = await c.Client.GetRooms();

					lock(Rooms)
					{

						var roomList = from r in rooms
										where !r.Closed
										orderby r.Count descending
										select new RoomListItemViewModel(c, r);
	
						Rooms.Clear();

						if (roomList != null)
						{
							foreach (var r in roomList)
								Rooms.Add(r);
						}

						IsLoading = false;
						RaisePropertyChanged (() => IsLoading);

						//RaisePropertyChanged(() => Rooms);
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