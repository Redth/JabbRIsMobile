using System;
using System.Linq;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Messages;
using JabbrMobile.Common.Models;

namespace JabbrMobile.Common.Services
{
	public class JabbrService : IJabbrService
	{
		IMvxMessenger _messenger;
		ISettingsService _settings;

		public JabbrService(IMvxMessenger messenger)
		{
			_messenger = messenger;
			//_settings = settings;

			_messenger.Subscribe<AccountMessage> (msg => {

				if (msg.Type == AccountMessage.ActionType.Add
				    && !clients.ContainsKey(msg.Account.Id))
				{
					AddClient(msg.Account);
				}
			});
		}


		Dictionary<string, JabbrClientWrapper> clients = new Dictionary<string, JabbrClientWrapper>();

		public IEnumerable<JabbrClientWrapper> Clients
		{
			get { return clients.Values; }
		}

		public void AddClient(Account account)
		{
			clients.Add(account.Id, new JabbrClientWrapper(account));
		}

	}


	public class JabbrClientWrapper
	{
		JabbRClient client;

		public Account Account { get; set; }

		public IMvxMessenger Messenger { get; set; }

		public JabbrClientWrapper(Account account)
		{
			RoomsIn = new List<JabbR.Client.Models.Room> ();

			this.Account = account;
			Connect ();
		}

		public string UserId { get; private set; }
		public List<JabbR.Client.Models.Room> RoomsIn { get; private set; }

		public void Connect()
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

			client.Connect (Account.Username, Account.Password).ContinueWith(t => {

				var ex = t.Exception;

				var result = t.Result;

				if (result != null)
				{
					this.UserId = result.UserId;

					//Add us into the result's Rooms
					RoomsIn.AddRange (result.Rooms);

					Log ("Connected> " + this.UserId ?? "" + " -> Rooms: " + RoomsIn.Count);
				}
			});
		}

		void HandleFlagChanged (JabbR.Client.Models.User user, string flag)
		{
			Log ("FlagChanged> " + user.Name + " -> " + flag);
		}

		void HandleAddMessageContent (string arg1, string arg2, string arg3)
		{
			Log ("AddMessageContent> " + arg1 + " -> " + arg2  + " -> " + arg3);
		}

		void HandleUserTyping (JabbR.Client.Models.User user, string arg2)
		{

		}

		void HandleUsersInactive (IEnumerable<JabbR.Client.Models.User> users)
		{
			Log ("UsersInactive> " + string.Join (", ", (from u in users select u.Name)));
		}

		void HandleUsernameChanged (string arg1, JabbR.Client.Models.User user, string arg3)
		{
			Log ("UsernameChanged> " + arg1 + " -> " + user.Name + " -> " + arg3);
		}

		void HandleUserLeft (JabbR.Client.Models.User user, string arg2)
		{
			Log ("UserLeft> " + user.Name + " -> " + arg2);
		}

		void HandleUserJoined (JabbR.Client.Models.User user, string arg2, bool arg3)
		{
			Log ("UserJoined> " + user.Name + " -> " + arg2 + " -> " + arg3);
		}

		void HandleUserActivityChanged (JabbR.Client.Models.User user)
		{
			Log ("UserActivityChanged> " + user.Name);
		}

		void HandleTopicChanged (JabbR.Client.Models.Room room)
		{
			Log ("TopicChanged> " + room.Name);
		}

		/*void HandleStateChanged (Microsoft.StateChange obj)
		{

		}*/

		void HandleRoomCountChanged (JabbR.Client.Models.Room room, int count)
		{
			Log ("RoomCountChanged> " + room.Name + " -> " + count);
		}

		void HandlePrivateMessage (string arg1, string arg2, string arg3)
		{
			Log ("PrivateMessage> " + arg1 + " -> " + arg2 + " -> " + arg3);
		}

		void HandleOwnerRemoved (JabbR.Client.Models.User user, string arg2)
		{
			Log ("OwnerRemoved> " + user.Name + " -> " + arg2);
		}

		void HandleOwnerAdded (JabbR.Client.Models.User user, string arg2)
		{
			Log ("OwnerAdded> " + user.Name + " -> " + arg2);
		}

		void HandleNoteChanged (JabbR.Client.Models.User user, string note)
		{
			Log ("NoteChanged> " + user.Name + " -> " + note);
		}

		void HandleMessageReceived (JabbR.Client.Models.Message message, string arg2)
		{
			Log ("MessageReceived> " + message.User.Name + ": " + message.Content + " -> " + arg2);
		}

		void HandleMeMessageReceived (string arg1, string arg2, string arg3)
		{
			Log ("MeMessageReceived> " + arg1 + " -> " + arg2 + " -> " + arg3);
		}

		void HandleLoggedOut (IEnumerable<string> arg1)
		{
			Log ("LoggedOut> " + string.Join(", ", arg1));
		}

		void HandleKicked (string obj)
		{
			Log ("Kicked> " + obj);
		}

		void HandleJoinedRoom (JabbR.Client.Models.Room room)
		{
			Log ("JoinedRoom> " + room.Name);
		}

		void HandleDisconnected ()
		{
			Log ("Disconnected>");
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