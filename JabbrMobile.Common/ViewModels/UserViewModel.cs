using System;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;

namespace JabbrMobile.Common.ViewModels
{
	public class UserViewModel : BaseViewModel
	{
		public UserViewModel(JabbR.Client.Models.User user) : base()
		{
			User = user;
		}

		public JabbR.Client.Models.User User { get; private set; }

		public string UsernameDisplay { get { return User.Name; } }

		public override bool Equals (object obj)
		{
			if (!(obj is UserViewModel))
				return false;

			var objUvm = obj as UserViewModel;

			if (objUvm == null)
				return false;

			return objUvm.User.Hash.Equals(this.User.Hash);
		}
	}

}