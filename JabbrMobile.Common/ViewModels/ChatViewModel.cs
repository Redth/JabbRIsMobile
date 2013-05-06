using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;
using System.Threading;
using System.Collections.ObjectModel;
using JabbrMobile.Common.Messages;

namespace JabbrMobile.Common.ViewModels
{
	public class ChatViewModel : BaseViewModel
	{
		public ChatViewModel(RoomViewModel room) : base()
		{
			Room = room;
		}

		public RoomViewModel Room { get; private set; }





	}

}