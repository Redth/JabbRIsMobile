using System;
using System.Linq;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Messages;
using JabbrMobile.Common.Models;
using JabbR.Client.Models;
using System.Collections.ObjectModel;

namespace JabbrMobile.Common.Services
{
	public class JabbrService : IJabbrService
	{
		IMvxMessenger _messenger;
		//ISettingsService _settings;

		public JabbrService(IMvxMessenger messenger)
		{
			Clients = new ObservableCollection<JabbrClientWrapper> ();

			_messenger = messenger;
			//_settings = settings;

			_messenger.Subscribe<AccountMessage> (msg => {

				if (msg.Type == AccountMessage.ActionType.Add)
				{
					AddClient(msg.Account);
				}
			});
		}



		public ObservableCollection<JabbrClientWrapper> Clients 
		{
			get;
			set;
		}

		public void AddClient(Account account)
		{
			Clients.Add(new JabbrClientWrapper(account));
		}

	}


	public class JabbrClientWrapper
	{
		JabbRClient client;

		public Account Account { get; set; }

		public IMvxMessenger Messenger { get; set; }

		public JabbrClientWrapper(Account account)
		{
			RoomsIn = new ObservableCollection<Room> ();

			this.Account = account;
			Connect ();
		}

		public string UserId { get; private set; }
		public ObservableCollection<JabbR.Client.Models.Room> RoomsIn { get; private set; }

		async void Connect()
		{
			client = new JabbRClient (this.Account.Url);
			client.AddMessageContent += HandleAddMessageContent;
			//client.Disconnected += HandleDisconnected;
			client.FlagChanged += HandleFlagChanged;
			client.JoinedRoom += HandleJoinedRoom;
			client.Kicked += HandleKicked;
			client.LoggedOut += HandleLoggedOut;
			client.MeMessageReceived += HandleMeMessageReceived; 
			client.MessageReceived += HandleMessageReceived;
			client.NoteChanged += HandleNoteChanged;
			client.OwnerAdded += HandleOwnerAdded;
			client.OwnerRemoved += HandleOwnerRemoved;
			client.PrivateMessage += HandlePrivateMessage;
			client.RoomCountChanged += HandleRoomCountChanged;
			//client.StateChanged += HandleStateChanged;
			client.TopicChanged += HandleTopicChanged;
			client.UserActivityChanged += HandleUserActivityChanged;
			client.UserJoined += HandleUserJoined;
			client.UserLeft += HandleUserLeft;
			client.UsernameChanged += HandleUsernameChanged;
			client.UsersInactive += HandleUsersInactive;
			client.UserTyping += HandleUserTyping;

			LogOnInfo logonInfo = null;

			try
			{
				logonInfo = await client.Connect (Account.Username, Account.Password);
			}
			catch (Exception ex)
			{
				Log ("Connect Exception: " + ex);
			}

			if (logonInfo != null)
			{
				this.UserId = logonInfo.UserId;

				//Add us into the result's Rooms
				foreach (var r in logonInfo.Rooms)
					RoomsIn.Add (r);

				Log ("Connected> " + this.UserId ?? "" + " -> Rooms: " + RoomsIn.Count);
				Messenger.Publish (new JabbrConnectedMessage (this, this, this.UserId, RoomsIn));
			}
		}

		void HandleFlagChanged (JabbR.Client.Models.User user, string flag)
		{
			Log ("FlagChanged> " + user.Name + " -> " + flag);
			Messenger.Publish (new JabbrFlagChangedMessage (this, this, user, flag));
		}

		void HandleAddMessageContent (string messageId, string extractedContent, string roomName)
		{
			Log ("AddMessageContent> " + messageId + " -> " + extractedContent  + " -> " + roomName);
			Messenger.Publish (new JabbrAddMessageContentMessage (this, this, messageId, extractedContent, roomName));
		}

		void HandleUserTyping (JabbR.Client.Models.User user, string roomName)
		{
			Messenger.Publish (new JabbrUserTypingMessage (this, this, user, roomName));
		}

		void HandleUsersInactive (IEnumerable<User> users)
		{
			Log ("UsersInactive> " + string.Join (", ", (from u in users select u.Name)));
			Messenger.Publish (new JabbrUsersInactiveMessage (this, this, users.ToList()));
		}

		void HandleUsernameChanged (string oldUsername, JabbR.Client.Models.User user, string roomName)
		{
			Log ("UsernameChanged> " + oldUsername + " -> " + user.Name + " -> " + roomName);
			Messenger.Publish(new JabbrUsernameChangedMessage(this, this, oldUsername, user, roomName)); 
		}

		void HandleUserLeft (JabbR.Client.Models.User user, string roomName)
		{
			Log ("UserLeft> " + user.Name + " -> " + roomName);
			Messenger.Publish(new JabbrUserLeftMessage(this, this, user, roomName));
		}

		void HandleUserJoined (JabbR.Client.Models.User user, string roomName, bool isOwner)
		{
			Log ("UserJoined> " + user.Name + " -> " + roomName + " -> " + isOwner);
			Messenger.Publish(new JabbrUserJoinedMessage(this, this, user, roomName, isOwner));
		}

		void HandleUserActivityChanged (JabbR.Client.Models.User user)
		{
			Log ("UserActivityChanged> " + user.Name);
			Messenger.Publish(new JabbrUserActivityChangedMessage(this, this, user));
		}

		void HandleTopicChanged (JabbR.Client.Models.Room room)
		{
			Log ("TopicChanged> " + room.Name);
			Messenger.Publish(new JabbrTopicChangedMessage(this, this, room));
		}

		/*void HandleStateChanged (Microsoft.StateChange obj)
		{

		}*/

		void HandleRoomCountChanged (JabbR.Client.Models.Room room, int count)
		{
			Log ("RoomCountChanged> " + room.Name + " -> " + count);
			Messenger.Publish(new JabbrRoomCountChangedMessage(this, this, room, count));
		}

		void HandlePrivateMessage (string fromUser, string toUser, string message)
		{
			Log ("PrivateMessage> " + fromUser + " -> " + toUser + " -> " + message);
			Messenger.Publish(new JabbrPrivateMessageMessage(this, this, fromUser, toUser, message));
		}

		void HandleOwnerRemoved (JabbR.Client.Models.User user, string roomName)
		{
			Log ("OwnerRemoved> " + user.Name + " -> " + roomName);
			Messenger.Publish(new JabbrOwnerRemovedMessage(this, this, user, roomName));
		}

		void HandleOwnerAdded (JabbR.Client.Models.User user, string roomName)
		{
			Log ("OwnerAdded> " + user.Name + " -> " + roomName);
			Messenger.Publish(new JabbrOwnerAddedMessage(this, this, user, roomName));
		}

		void HandleNoteChanged (JabbR.Client.Models.User user, string note)
		{
			Log ("NoteChanged> " + user.Name + " -> " + note);
			Messenger.Publish(new JabbrNoteChangedMessage(this, this, user, note));
		}

		void HandleMessageReceived (JabbR.Client.Models.Message message, string roomName)
		{
			Log ("MessageReceived> " + message.User.Name + ": " + message.Content + " -> " + roomName);
			Messenger.Publish(new JabbrMessageReceivedMessage(this, this, message, roomName));
		}

		void HandleMeMessageReceived (string user, string content, string roomName)
		{
			Log ("MeMessageReceived> " + user + " -> " + content + " -> " + roomName);
			Messenger.Publish (new JabbrMeMessageReceivedMessage (this, this, user, content, roomName));
		}

		void HandleLoggedOut (IEnumerable<string> roomNames)
		{
			Log ("LoggedOut> " + string.Join(", ", roomNames));
			Messenger.Publish (new JabbrLoggedOutMessage (this, this, roomNames.ToList()));
		}

		void HandleKicked (string roomName)
		{
			Log ("Kicked> " + roomName);
			Messenger.Publish (new JabbrKickedMessage (this, this, roomName));
		}

		void HandleJoinedRoom (JabbR.Client.Models.Room room)
		{
			Log ("JoinedRoom> " + room.Name);
			Messenger.Publish(new JabbrJoinedRoomMessage(this, this, room));
		}

		void HandleDisconnected ()
		{
			Log ("Disconnected>");
			Messenger.Publish(new JabbrDisconnectedMessage(this, this));
		}

		public void Disconnect()
		{
			client.AddMessageContent -= HandleAddMessageContent;
			//client.Disconnected -= HandleDisconnected;
			client.FlagChanged -= HandleFlagChanged;
			client.JoinedRoom -= HandleJoinedRoom;
			client.Kicked -= HandleKicked;
			client.LoggedOut -= HandleLoggedOut;
			client.MeMessageReceived -= HandleMeMessageReceived; 
			client.MessageReceived -= HandleMessageReceived;
			client.NoteChanged -= HandleNoteChanged;
			client.OwnerAdded -= HandleOwnerAdded;
			client.OwnerRemoved -= HandleOwnerRemoved;
			client.PrivateMessage -= HandlePrivateMessage;
			client.RoomCountChanged -= HandleRoomCountChanged;
			//client.StateChanged -= HandleStateChanged;
			client.TopicChanged -= HandleTopicChanged;
			client.UserActivityChanged -= HandleUserActivityChanged;
			client.UserJoined -= HandleUserJoined;
			client.UserLeft -= HandleUserLeft;
			client.UsernameChanged -= HandleUsernameChanged;
			client.UsersInactive -= HandleUsersInactive;
			client.UserTyping -= HandleUserTyping;

			client.Disconnect ();
		}

		void Log(string msg)
		{
			Console.WriteLine (msg);
		}
	}
}