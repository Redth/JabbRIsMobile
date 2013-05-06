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
using Cirrious.CrossCore;

namespace JabbrMobile.Common.Services
{
	public class JabbrService : IJabbrService
	{
		MvxSubscriptionToken mvxSubAccountMessages;

		public JabbrService()
		{
			Connections = new ObservableCollection<JabbrConnection> ();

			var messenger = Mvx.Resolve<IMvxMessenger> ();
			mvxSubAccountMessages = messenger.Subscribe<AccountMessage> (msg => {

				if (msg.Type == AccountMessage.ActionType.Add)
					AddClient(msg.Account);

				//TODO: Remove accounts
			});
		}

		public ObservableCollection<JabbrConnection> Connections { get; set; }

		public void AddClient(Account account)
		{
			Connections.Add(new JabbrConnection(account));
		}

	}


	public class JabbrConnection
	{
		IMvxMessenger _messenger;
		JabbRClient _client;

		public Account Account { get; set; }

		public JabbrConnection(Account account)
		{
			_messenger = Mvx.Resolve<IMvxMessenger> ();

			RoomsIn = new ObservableCollection<Room> ();

			this.Account = account;
			Connect ();
		}

		public string UserId { get; private set; }
		public ObservableCollection<JabbR.Client.Models.Room> RoomsIn { get; private set; }
		public JabbRClient Client { get { return _client; } }

		async void Connect()
		{
			_client = new JabbRClient (this.Account.Url);
			_client.AddMessageContent += HandleAddMessageContent;
			//client.Disconnected += HandleDisconnected;
			_client.FlagChanged += HandleFlagChanged;
			_client.JoinedRoom += HandleJoinedRoom;
			_client.Kicked += HandleKicked;
			_client.LoggedOut += HandleLoggedOut;
			_client.MeMessageReceived += HandleMeMessageReceived; 
			_client.MessageReceived += HandleMessageReceived;
			_client.NoteChanged += HandleNoteChanged;
			_client.OwnerAdded += HandleOwnerAdded;
			_client.OwnerRemoved += HandleOwnerRemoved;
			_client.PrivateMessage += HandlePrivateMessage;
			_client.RoomCountChanged += HandleRoomCountChanged;
			//client.StateChanged += HandleStateChanged;
			_client.TopicChanged += HandleTopicChanged;
			_client.UserActivityChanged += HandleUserActivityChanged;
			_client.UserJoined += HandleUserJoined;
			_client.UserLeft += HandleUserLeft;
			_client.UsernameChanged += HandleUsernameChanged;
			_client.UsersInactive += HandleUsersInactive;
			_client.UserTyping += HandleUserTyping;

			LogOnInfo logonInfo = null;

			try
			{
				logonInfo = await _client.Connect (Account.Username, Account.Password);
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
				{
					Console.WriteLine ("Rooms In: " + r.Name);
					RoomsIn.Add (r);
				}

				Log ("Connected> " + this.UserId ?? "" + " -> Rooms: " + RoomsIn.Count);
				_messenger.Publish (new JabbrConnectedMessage (this, this, this.UserId, RoomsIn));
			}
		}

		void HandleFlagChanged (JabbR.Client.Models.User user, string flag)
		{
			Log ("FlagChanged> " + user.Name + " -> " + flag);
			_messenger.Publish (new JabbrFlagChangedMessage (this, this, user, flag));
		}

		void HandleAddMessageContent (string messageId, string extractedContent, string roomName)
		{
			Log ("AddMessageContent> " + messageId + " -> " + extractedContent  + " -> " + roomName);
			_messenger.Publish (new JabbrAddMessageContentMessage (this, this, messageId, extractedContent, roomName));
		}

		void HandleUserTyping (JabbR.Client.Models.User user, string roomName)
		{
			_messenger.Publish (new JabbrUserTypingMessage (this, this, user, roomName));
		}

		void HandleUsersInactive (IEnumerable<User> users)
		{
			Log ("UsersInactive> " + string.Join (", ", (from u in users select u.Name)));
			_messenger.Publish (new JabbrUsersInactiveMessage (this, this, users.ToList()));
		}

		void HandleUsernameChanged (string oldUsername, JabbR.Client.Models.User user, string roomName)
		{
			Log ("UsernameChanged> " + oldUsername + " -> " + user.Name + " -> " + roomName);
			_messenger.Publish(new JabbrUsernameChangedMessage(this, this, oldUsername, user, roomName)); 
		}

		void HandleUserLeft (JabbR.Client.Models.User user, string roomName)
		{
			Log ("UserLeft> " + user.Name + " -> " + roomName);
			_messenger.Publish(new JabbrUserLeftMessage(this, this, user, roomName));
		}

		void HandleUserJoined (JabbR.Client.Models.User user, string roomName, bool isOwner)
		{
			Log ("UserJoined> " + user.Name + " -> " + roomName + " -> " + isOwner);
			_messenger.Publish(new JabbrUserJoinedMessage(this, this, user, roomName, isOwner));
		}

		void HandleUserActivityChanged (JabbR.Client.Models.User user)
		{
			Log ("UserActivityChanged> " + user.Name);
			_messenger.Publish(new JabbrUserActivityChangedMessage(this, this, user));
		}

		void HandleTopicChanged (JabbR.Client.Models.Room room)
		{
			Log ("TopicChanged> " + room.Name);
			_messenger.Publish(new JabbrTopicChangedMessage(this, this, room));
		}

		/*void HandleStateChanged (Microsoft.StateChange obj)
		{

		}*/

		void HandleRoomCountChanged (JabbR.Client.Models.Room room, int count)
		{
			Log ("RoomCountChanged> " + room.Name + " -> " + count);
			_messenger.Publish(new JabbrRoomCountChangedMessage(this, this, room, count));
		}

		void HandlePrivateMessage (string fromUser, string toUser, string message)
		{
			Log ("PrivateMessage> " + fromUser + " -> " + toUser + " -> " + message);
			_messenger.Publish(new JabbrPrivateMessageMessage(this, this, fromUser, toUser, message));
		}

		void HandleOwnerRemoved (JabbR.Client.Models.User user, string roomName)
		{
			Log ("OwnerRemoved> " + user.Name + " -> " + roomName);
			_messenger.Publish(new JabbrOwnerRemovedMessage(this, this, user, roomName));
		}

		void HandleOwnerAdded (JabbR.Client.Models.User user, string roomName)
		{
			Log ("OwnerAdded> " + user.Name + " -> " + roomName);
			_messenger.Publish(new JabbrOwnerAddedMessage(this, this, user, roomName));
		}

		void HandleNoteChanged (JabbR.Client.Models.User user, string note)
		{
			Log ("NoteChanged> " + user.Name + " -> " + note);
			_messenger.Publish(new JabbrNoteChangedMessage(this, this, user, note));
		}

		void HandleMessageReceived (JabbR.Client.Models.Message message, string roomName)
		{
			Log ("MessageReceived> " + message.User.Name + ": " + message.Content + " -> " + roomName);
			_messenger.Publish(new JabbrMessageReceivedMessage(this, this, message, roomName));
		}

		void HandleMeMessageReceived (string user, string content, string roomName)
		{
			Log ("MeMessageReceived> " + user + " -> " + content + " -> " + roomName);
			_messenger.Publish (new JabbrMeMessageReceivedMessage (this, this, user, content, roomName));
		}

		void HandleLoggedOut (IEnumerable<string> roomNames)
		{
			Log ("LoggedOut> " + string.Join(", ", roomNames));
			_messenger.Publish (new JabbrLoggedOutMessage (this, this, roomNames.ToList()));
		}

		void HandleKicked (string roomName)
		{
			Log ("Kicked> " + roomName);
			_messenger.Publish (new JabbrKickedMessage (this, this, roomName));
		}

		void HandleJoinedRoom (JabbR.Client.Models.Room room)
		{
			Log ("JoinedRoom> " + room.Name);
			_messenger.Publish(new JabbrJoinedRoomMessage(this, this, room));
		}

		void HandleDisconnected ()
		{
			Log ("Disconnected>");
			_messenger.Publish(new JabbrDisconnectedMessage(this, this));
		}

		public void Disconnect()
		{
			_client.AddMessageContent -= HandleAddMessageContent;
			//client.Disconnected -= HandleDisconnected;
			_client.FlagChanged -= HandleFlagChanged;
			_client.JoinedRoom -= HandleJoinedRoom;
			_client.Kicked -= HandleKicked;
			_client.LoggedOut -= HandleLoggedOut;
			_client.MeMessageReceived -= HandleMeMessageReceived; 
			_client.MessageReceived -= HandleMessageReceived;
			_client.NoteChanged -= HandleNoteChanged;
			_client.OwnerAdded -= HandleOwnerAdded;
			_client.OwnerRemoved -= HandleOwnerRemoved;
			_client.PrivateMessage -= HandlePrivateMessage;
			_client.RoomCountChanged -= HandleRoomCountChanged;
			//client.StateChanged -= HandleStateChanged;
			_client.TopicChanged -= HandleTopicChanged;
			_client.UserActivityChanged -= HandleUserActivityChanged;
			_client.UserJoined -= HandleUserJoined;
			_client.UserLeft -= HandleUserLeft;
			_client.UsernameChanged -= HandleUsernameChanged;
			_client.UsersInactive -= HandleUsersInactive;
			_client.UserTyping -= HandleUserTyping;

			_client.Disconnect ();
		}

		void Log(string msg)
		{
			Console.WriteLine (msg);
		}
	}
}