using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using JabbR.Client.Models;
using JabbrMobile.Common.Messages;
using JabbrMobile.Common.Services;

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

			TypedMessage = string.Empty;
			Messages = new ObservableCollection<MessageViewModel> ();

			subTokMessageReceived = Messenger.Subscribe<JabbrMessageReceivedMessage> (msg => {

				this.InvokeOnMainThread(() => {
					lock (Messages)
					{
						Mvx.Trace(msg.Sender + "> " + msg.Message.HtmlEncoded);
						Messages.Add(new MessageViewModel(msg.Message));
					}
				});
			});

			subTokUserJoin = Messenger.Subscribe<JabbrUserJoinedMessage> (msg => {

			});

			subTokUserLeft = Messenger.Subscribe<JabbrUserLeftMessage> (msg => {

			});

			try
			{
				lock (Messages)
				{
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

		public string TypedMessage { get; set; }

		public ObservableCollection<MessageViewModel> Messages { get; set; }

		public string ServerDisplayName { 
			get {
				return Jabbr.Account.Url.Replace ("https:", "").Replace ("http:", "").Trim ('/');
			}
		}



		public ICommand TypingActivityCommand
		{
			get
			{
				return new MvxCommand (async() => {

					Mvx.Trace("Typing...");

					if (IsTyping)
						return;

					Mvx.Trace("Typing... YES");

					IsTyping = true;

					//Tell JabbR we are typing
					await Jabbr.Client.SetTyping (Room.Name);

					RaisePropertyChanged (() => IsTyping);
				});
			}
		}

		public ICommand SendMessageCommand
		{
			get
			{
				return new MvxCommand(async() => {

					var msgToSend = TypedMessage;

					//Clear out hte msg
					TypedMessage = string.Empty;
					RaisePropertyChanged(() => TypedMessage);

					//Send message to jabbr
					await Jabbr.Client.Send (msgToSend, Room.Name);

					//Change typing status
					IsTyping = false;
					RaisePropertyChanged(() => IsTyping);
				});
			}
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
				}
			}
			catch (Exception ex)
			{
				Mvx.Error("LoadMoreHistory Exception: " + ex);
			}
		}
	}

}