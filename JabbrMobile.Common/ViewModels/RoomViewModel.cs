using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;
using System.Collections.ObjectModel;
using JabbrMobile.Common.Messages;

namespace JabbrMobile.Common.ViewModels
{
	public class RoomViewModel : BaseViewModel
	{
		MvxSubscriptionToken subTokMessageReceived;

		public RoomViewModel() : base()
		{
			subTokMessageReceived = Messenger.Subscribe<JabbrMessageReceivedMessage> (msg => {

				lock (Messages)
					Messages.Add(msg.Message);
			});

		}

		public JabbrConnection Jabbr { get;set; }
		public Room Room { get;set; }
		public bool IsTyping { get;set; }

		public ObservableCollection<JabbR.Client.Models.Message> Messages { get; set; }

		public string ServerDisplayName { 
			get {
				return Jabbr.Account.Url.Replace ("https:", "").Replace ("http:", "").Trim ('/');
			}
		}

		public async void TypingActivityCommand()
		{
			if (IsTyping)
				return;

			IsTyping = true;

			//Tell JabbR we are typing
			await Jabbr.Client.SetTyping (Room.Name);

			RaisePropertyChanged (() => IsTyping);

		}

		public async void SendMessageCommand(string message)
		{
			//Send message to jabbr
			await Jabbr.Client.Send (message, Room.Name);

			IsTyping = false;

			RaisePropertyChanged(() => IsTyping);
		}

		public async void LoadMoreHistoryCommand()
		{
			string fromId = null;

			JabbR.Client.Models.Message lastMsg = null;

			lock (Messages)
				lastMsg = Messages.LastOrDefault ();

			if (lastMsg != null)
				fromId = lastMsg.Id;

			var msgs = await Jabbr.Client.GetPreviousMessages (fromId);

			if (msgs == null)
				return;

			lock (Messages)
			{
				foreach (var msg in msgs.Reverse())
					Messages.Insert (0, msg);
			}
		}
	}

}