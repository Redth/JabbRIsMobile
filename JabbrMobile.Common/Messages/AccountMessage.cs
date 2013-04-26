using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Models;

namespace JabbrMobile.Common.Messages
{
	public class AccountMessage : MvxMessage
	{
		public AccountMessage(object sender) : base(sender)
		{
		}

		public Account Account { get;set; }
		public ActionType Type { get; set; }

		public enum ActionType
		{
			Add,
			Remove
		}
	}
}