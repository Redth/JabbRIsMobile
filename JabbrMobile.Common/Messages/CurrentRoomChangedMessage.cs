using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Models;
using JabbrMobile.Common.ViewModels;

namespace JabbrMobile.Common.Messages
{
	public class CurrentRoomChangedMessage : MvxMessage
	{
		public CurrentRoomChangedMessage(object sender) : base(sender)
		{
		}

		public RoomViewModel OldRoom { get;set; }
		public RoomViewModel NewRoom { get;set; }
	}
}