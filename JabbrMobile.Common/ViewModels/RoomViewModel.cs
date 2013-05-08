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
using System.Windows.Input;

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

			subTokMessageReceived = Messenger.SubscribeOnMainThread<JabbrMessageReceivedMessage> (msg => {

				lock (Messages)
					Messages.Add(new MessageViewModel(msg.Message));

			});

			subTokUserJoin = Messenger.Subscribe<JabbrUserJoinedMessage> (msg => {

			});

			subTokUserLeft = Messenger.Subscribe<JabbrUserLeftMessage> (msg => {

			});

			LoadRoom ();
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
				return new MvxCommand ( async() => {
					if (IsTyping)
						return;

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
				return new MvxCommand( async () => {

					var msgToSend = TypedMessage;

					TypedMessage = string.Empty;
					RaisePropertyChanged(() => TypedMessage);

					Mvx.Trace ("Send Message: " + msgToSend);

					//Send message to jabbr
					await Jabbr.Client.Send (msgToSend, Room.Name);

					IsTyping = false;

					RaisePropertyChanged(() => IsTyping);
			
				});
			}
		}

		public async void LoadRoom()
		{
			var room = await Jabbr.Client.GetRoomInfo (this.Room.Name);

			if (room == null)
				return;

			this.Room = room;

			if (room.RecentMessages != null && room.RecentMessages.Count() > 0)
			{
				lock(Messages)
				{
					foreach (var msg in room.RecentMessages.Reverse())
						Messages.Insert (0, new MessageViewModel(msg));
				}
			}
		}

		public ICommand LoadMoreHistoryCommand
		{
			get
			{
				return new MvxCommand ( async() => {
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
				});
			}
		}
	}

}