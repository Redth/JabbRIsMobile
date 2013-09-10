using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Models;
using JabbrMobile.Common.Services;
using JabbR.Client.Models;
using System.Collections.Generic;

namespace JabbrMobile.Common.Messages
{
	public class JabbrMessage : MvxMessage
	{
		public JabbrMessage(object sender, JabbrConnection jabbr) : base(sender)
		{
			this.Jabbr = jabbr;
		}

		public JabbrConnection Jabbr { get;set; }
	}
	
	public class JabbrAddMessageContentMessage : JabbrMessage
	{
		public JabbrAddMessageContentMessage(object sender, JabbrConnection jabbr, string messageId, string extractedContent, string roomName)
			: base (sender, jabbr)
		{
			this.MessageId = messageId;
			this.ExtractedContent = extractedContent;
			this.RoomName = roomName;
		}

		public string MessageId { get;set; }
		public string ExtractedContent { get;set; }
		public string RoomName { get;set; }
	}

	public class JabbrConnectedMessage : JabbrMessage
	{
		public JabbrConnectedMessage(object sender, JabbrConnection jabbr, string userId, IEnumerable<Room> rooms)
			: base(sender, jabbr)
		{
			this.UserId = userId;
			this.Rooms = rooms;
		}

		public string UserId { get;set; }
		public IEnumerable<Room> Rooms { get;set; }
	}


	public class JabbrDisconnectedMessage : JabbrMessage
	{
		public JabbrDisconnectedMessage(object sender, JabbrConnection jabbr)
			: base(sender, jabbr)
		{

		}

	}

	public class JabbrFlagChangedMessage : JabbrMessage
	{
		public JabbrFlagChangedMessage(object sender, JabbrConnection jabbr, User user, string flag)
			: base(sender, jabbr)
		{
			this.User = user;
			this.Flag = flag;
		}

		public User User { get;set; }
		public string Flag { get;set; }
	}

	public class JabbrJoinedRoomMessage : JabbrMessage
	{
		public JabbrJoinedRoomMessage(object sender, JabbrConnection jabbr, Room room)
			: base(sender, jabbr)
		{
			this.Room = room;
		}

		public Room Room { get; set; }
	}

	public class JabbrKickedMessage : JabbrMessage
	{
		public JabbrKickedMessage(object sender, JabbrConnection jabbr, string roomName)
			: base(sender, jabbr)
		{
			this.RoomName = roomName;
		}

		public string RoomName { get;set; }
	}

	public class JabbrLoggedOutMessage : JabbrMessage
	{
		public JabbrLoggedOutMessage(object sender, JabbrConnection jabbr, List<string> roomNames)
			: base(sender, jabbr)
		{
			this.RoomNames = roomNames;
		}

		public List<string> RoomNames { get;set; }
	}

	public class JabbrMeMessageReceivedMessage : JabbrMessage
	{
		public JabbrMeMessageReceivedMessage(object sender, JabbrConnection jabbr, string user, string content, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.Content = content;
			this.RoomName = roomName;
		}

		public string User { get;set; }
		public string Content { get;set; }
		public string RoomName { get;set; }
	}

	public class JabbrMessageReceivedMessage : JabbrMessage
	{
		public JabbrMessageReceivedMessage(object sender, JabbrConnection jabbr, JabbR.Client.Models.Message message, string roomName)
			: base(sender, jabbr)
		{
			this.Message = message;
			this.RoomName = roomName;
		}

		public JabbR.Client.Models.Message Message { get;set; }
		public string RoomName { get;set; }
	}

	public class JabbrNoteChangedMessage : JabbrMessage
	{
		public JabbrNoteChangedMessage(object sender, JabbrConnection jabbr, User user, string note)
			: base(sender, jabbr)
		{
			this.User = user;
			this.Note = note;
		}

		public User User { get;set; }
		public string Note { get;set; }
	}

	public class JabbrOwnerAddedMessage : JabbrMessage
	{
		public JabbrOwnerAddedMessage(object sender, JabbrConnection jabbr, User user, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
	}

	public class JabbrOwnerRemovedMessage : JabbrMessage
	{
		public JabbrOwnerRemovedMessage(object sender, JabbrConnection jabbr, User user, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
	}

	public class JabbrPrivateMessageMessage : JabbrMessage
	{
		public JabbrPrivateMessageMessage(object sender, JabbrConnection jabbr, string fromUser, string toUser, string message)
			: base(sender, jabbr)
		{
			this.From = fromUser;
			this.To = toUser;
			this.Message = message;
		}

		public string From { get;set;}
		public string To { get;set; }
		public string Message { get;set; }
	}

	public class JabbrRoomCountChangedMessage : JabbrMessage
	{
		public JabbrRoomCountChangedMessage(object sender, JabbrConnection jabbr, Room room, int count)
			: base(sender, jabbr)
		{
			this.Room = room;
			this.Count = count;
		}

		public Room Room { get;set; }
		public int Count { get; set; }
	}

	public class JabbrStateChangedMessage : JabbrMessage
	{
		public JabbrStateChangedMessage(object sender, JabbrConnection jabbr)
			: base(sender, jabbr)
		{
		}
	}

	public class JabbrTopicChangedMessage : JabbrMessage
	{
		public JabbrTopicChangedMessage(object sender, JabbrConnection jabbr, string roomName, string topic, string who)
			: base(sender, jabbr)
		{
			this.RoomName = roomName;
			this.Topic = topic;
			this.Who = who;
		}

		public string RoomName { get;set; }
		public string Topic { get;set; }
		public string Who { get;set; }
	}

	public class JabbrUserActivityChangedMessage : JabbrMessage
	{
		public JabbrUserActivityChangedMessage(object sender, JabbrConnection jabbr, User user)
			: base(sender, jabbr)
		{
			this.User = user;
		}

		public User User { get;set; }
	}

	public class JabbrUserJoinedMessage : JabbrMessage
	{
		public JabbrUserJoinedMessage(object sender, JabbrConnection jabbr, User user, string roomName, bool isOwner)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
			this.IsOwner = isOwner;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
		public bool IsOwner { get;set; }
	}

	public class JabbrUserLeftMessage : JabbrMessage
	{
		public JabbrUserLeftMessage(object sender, JabbrConnection jabbr, User user, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
	}

	public class JabbrUsernameChangedMessage : JabbrMessage
	{
		public JabbrUsernameChangedMessage(object sender, JabbrConnection jabbr, string oldUsername, User user, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
			this.OldUsername = oldUsername;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
		public string OldUsername { get;set; }
	}

	public class JabbrUsersInactiveMessage : JabbrMessage
	{
		public JabbrUsersInactiveMessage(object sender, JabbrConnection jabbr, List<User> users)
			: base(sender, jabbr)
		{
			this.Users = users;
		}

		public List<User> Users { get;set; }
	}

	public class JabbrUserTypingMessage : JabbrMessage
	{
		public JabbrUserTypingMessage(object sender, JabbrConnection jabbr, User user, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
	}
}