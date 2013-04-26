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
	public class HomeViewModel : MvxViewModel
	{
		ISettingsService _settings;
		IJabbrService _service;
		IMvxMessenger _messenger;

		public HomeViewModel(ISettingsService settings, IJabbrService service, IMvxMessenger messenger)
		{
			_settings = settings;
			_service = service;
			_messenger = messenger;

			_messenger.Subscribe<JabbrConnectedMessage> (msg => RaisePropertyChanged ("Rooms"));




			try
			{
				_service.AddClient(new JabbrMobile.Common.Models.Account()
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

		public List<RoomInfo> Rooms
		{
			get
			{
				var rooms = new List<RoomInfo> ();

				foreach (var c in _service.Clients)
				{
					foreach (var r in c.RoomsIn)
						rooms.Add(new RoomInfo () { Room = r, Jabbr = c });
				}

				return rooms;
			}
		}

		public ISettingsService Settings 
		{ 
			get { return _settings; }
			set { _settings = value; }
		}

		public IJabbrService Service
		{
			get { return _service; }
			set { _service = value; }
		}
		 
	}

}