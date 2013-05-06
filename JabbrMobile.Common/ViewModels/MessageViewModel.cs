using System;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;

namespace JabbrMobile.Common.ViewModels
{
	public class MessageViewModel : BaseViewModel
	{
		public MessageViewModel(JabbR.Client.Models.Message message) : base()
		{
			Message = message;
		}

		public JabbR.Client.Models.Message Message { get; private set; }

		public string UsernameDisplay { get { return Message.User.Name; } }
		public string MessageBodyDisplay { get { return Message.Content; } }
	}

}