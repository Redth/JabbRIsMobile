using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Models;
using JabbR.Client.Models;

namespace JabbrMobile.Common.Messages
{
	public class UserSelectedMessage : MvxMessage
	{
		public UserSelectedMessage(object sender, User user) : base(sender)
		{
			this.User = user;
		}

		public User User { get; private set; }
	}
}