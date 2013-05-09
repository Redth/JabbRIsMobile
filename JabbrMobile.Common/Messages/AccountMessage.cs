using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Models;

namespace JabbrMobile.Common.Messages
{
	public class AccountsChangedMessage : MvxMessage
	{
		public AccountsChangedMessage(object sender) : base(sender)
		{
		}
	}
}