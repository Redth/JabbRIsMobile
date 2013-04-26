using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Models;
using JabbrMobile.Common.Services;
using JabbR.Client.Models;
using System.Collections.Generic;

namespace JabbrMobile.Common.Messages
{
	public class JabbrMessage : MvxMessage
	{
		public JabbrMessage(object sender, JabbrClientWrapper jabbr) : base(sender)
		{
			this.Jabbr = jabbr;
		}

		public JabbrClientWrapper Jabbr { get;set; }
	}
	
	public class JabbrAddMessageContentMessage : JabbrMessage
	{
		public JabbrAddMessageContentMessage(object sender, JabbrClientWrapper jabbr, string messageId, string extractedContent, string roomName)
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

	public class JabbrDisconnectedMessage : JabbrMessage
	{
		public JabbrDisconnectedMessage(object sender, JabbrClientWrapper jabbr)
			: base(sender, jabbr)
		{

		}

	}

	public class JabbrFlagChangedMessage : JabbrMessage
	{
		public JabbrFlagChangedMessage(object sender, JabbrClientWrapper jabbr, User user, string flag)
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
		public JabbrJoinedRoomMessage(object sender, JabbrClientWrapper jabbr, Room room)
			: base(sender, jabbr)
		{
			this.Room = room;
		}

		public Room Room { get; set; }
	}

	public class JabbrKickedMessage : JabbrMessage
	{
		public JabbrKickedMessage(object sender, JabbrClientWrapper jabbr, string roomName)
			: base(sender, jabbr)
		{
			this.RoomName = roomName;
		}

		public string RoomName { get;set; }
	}

	public class JabbrLoggedOutMessage : JabbrMessage
	{
		public JabbrLoggedOutMessage(object sender, JabbrClientWrapper jabbr, List<string> roomNames)
			: base(sender, jabbr)
		{
			this.RoomNames = roomNames;
		}

		public List<string> RoomNames { get;set; }
	}

	public class JabbrMeMessageReceivedMessage : JabbrMessage
	{
		public JabbrMeMessageReceivedMessage(object sender, JabbrClientWrapper jabbr, string user, string content, string roomName)
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
		public JabbrMessageReceivedMessage(object sender, JabbrClientWrapper jabbr, JabbR.Client.Models.Message message, string roomName)
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
		public JabbrNoteChangedMessage(object sender, JabbrClientWrapper jabbr, User user, string note)
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
		public JabbrOwnerAddedMessage(object sender, JabbrClientWrapper jabbr, User user, string roomName)
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
		public JabbrOwnerRemovedMessage(object sender, JabbrClientWrapper jabbr, User user, string roomName)
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
		public JabbrPrivateMessageMessage(object sender, JabbrClientWrapper jabbr, string fromUser, string toUser, string message)
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
		public JabbrRoomCountChangedMessage(object sender, JabbrClientWrapper jabbr, Room room, int count)
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
		public JabbrStateChangedMessage(object sender, JabbrClientWrapper jabbr)
			: base(sender, jabbr)
		{
		}
	}

	public class JabbrTopicChangedMessage : JabbrMessage
	{
		public JabbrTopicChangedMessage(object sender, JabbrClientWrapper jabbr, Room room)
			: base(sender, jabbr)
		{
			this.Room = room;
		}

		public Room Room { get;set; }
	}

	public class JabbrUserActivityChangedMessage : JabbrMessage
	{
		public JabbrUserActivityChangedMessage(object sender, JabbrClientWrapper jabbr, User user)
			: base(sender, jabbr)
		{
			this.User = user;
		}

		public User User { get;set; }
	}

	public class JabbrUserJoinedMessage : JabbrMessage
	{
		public JabbrUserJoinedMessage(object sender, JabbrClientWrapper jabbr, User user, string roomName, bool isOwner)
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
		public JabbrUserLeftMessage(object sender, JabbrClientWrapper jabbr, User user, string roomName)
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
		public JabbrUsernameChangedMessage(object sender, JabbrClientWrapper jabbr, string oldUsername, User user, string roomName)
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
		public JabbrUsersInactiveMessage(object sender, JabbrClientWrapper jabbr, List<User> users)
			: base(sender, jabbr)
		{
			this.Users = users;
		}

		public List<User> Users { get;set; }
	}

	public class JabbrUserTypingMessage : JabbrMessage
	{
		public JabbrUserTypingMessage(object sender, JabbrClientWrapper jabbr, User user, string roomName)
			: base(sender, jabbr)
		{
			this.User = user;
			this.RoomName = roomName;
		}

		public User User { get;set; }
		public string RoomName { get;set; }
	}
}