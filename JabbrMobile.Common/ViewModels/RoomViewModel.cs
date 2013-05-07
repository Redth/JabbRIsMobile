using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;
using System.Collections.ObjectModel;
using JabbrMobile.Common.Messages;
using System.Threading.Tasks;
using Cirrious.CrossCore;

namespace JabbrMobile.Common.ViewModels
{
	public class RoomViewModel : BaseViewModel
	{
		MvxSubscriptionToken subTokMessageReceived;
		MvxSubscriptionToken subTokUserLeft;
		MvxSubscriptionToken subTokUserJoin;

		public RoomViewModel(JabbrConnection jabbr, Room room) : base()
		{
			Jabbr = jabbr;
			Room = room;

			TypingMessage = string.Empty;
			Messages = new ObservableCollection<MessageViewModel> ();

			subTokMessageReceived = Messenger.Subscribe<JabbrMessageReceivedMessage> (msg => {

				lock (Messages)
					Messages.Add(new MessageViewModel(msg.Message));

				RaisePropertyChanged(() => Messages);
			});

			subTokUserJoin = Messenger.Subscribe<JabbrUserJoinedMessage> (msg => {

			});

			subTokUserLeft = Messenger.Subscribe<JabbrUserLeftMessage> (msg => {

			});

			try
			{
				lock (Messages)
				{
                    if (Room.RecentMessages != null)
					    foreach (var msg in Room.RecentMessages)
						    Messages.Add (new MessageViewModel(msg));
				}
			}
			catch (Exception ex)
			{
				Mvx.Error ("RecentMessages Exception: " + ex);
			}

			RaisePropertyChanged (() => Messages);
		}

		public JabbrConnection Jabbr { get; private set; }
		public Room Room { get;set; }
		public bool IsTyping { get;set; }

		public string TypingMessage { get; set; }

		public ObservableCollection<MessageViewModel> Messages { get; set; }

		public string ServerDisplayName { 
			get {
				return Jabbr.Account.Url.Replace ("https:", "").Replace ("http:", "").Trim ('/');
			}
		}



		public async Task TypingActivityCommand()
		{
			if (IsTyping)
				return;

			IsTyping = true;

			//Tell JabbR we are typing
			await Jabbr.Client.SetTyping (Room.Name);

			RaisePropertyChanged (() => IsTyping);

		}

		public async Task SendMessageCommand()
		{
			Mvx.Trace ("Send Message: " + TypingMessage);

			//Send message to jabbr
			await Jabbr.Client.Send (this.TypingMessage, Room.Name);

			IsTyping = false;

			RaisePropertyChanged(() => IsTyping);
		}

		public async Task LoadMoreHistoryCommand()
		{
			Mvx.Trace ("Loading more History...");

			try
			{
				string fromId = string.Empty;

				MessageViewModel lastMsg = null;

				lock (Messages)
					lastMsg = Messages.LastOrDefault ();

				if (lastMsg != null && lastMsg.Message != null && lastMsg.Message.Id != null)
					fromId = lastMsg.Message.Id;

				var msgs = await Jabbr.Client.GetPreviousMessages(fromId);

				if (msgs == null)
				{
					Mvx.Trace ("No History Found");
					return;
				}

				lock (Messages)
				{
					Mvx.Trace ("Got {0} Messages", Messages.Count);

					foreach (var msg in msgs.Reverse())
						Messages.Insert (0, new MessageViewModel(msg));

					RaisePropertyChanged(() => Messages);
				}
			}
			catch (Exception ex)
			{
				Mvx.Error("LoadMoreHistory Exception: " + ex);
			}
		}
	}

}