using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbR.Client.Models;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JabbrMobile.Common.ViewModels
{
	public class EditAccountViewModel : BaseViewModel
	{
		public EditAccountViewModel() : base()
		{

		}

		protected override void InitFromBundle (IMvxBundle parameters)
		{
			if (parameters != null && parameters.Data != null && parameters.Data.ContainsKey("AccountId"))
			{
				var acctId = parameters.Data ["AccountId"];

				Account = Settings.Accounts.Where (a => a.Id.Equals(acctId)).FirstOrDefault ();
			}

			if (Account == null)
			{
				NewAccount = true;
				Account = new Account ();
			}
		}

		public bool NewAccount { get;set; }
		public Account Account { get; set; }

		public ICommand SaveCommand
		{
			get
			{
				return new MvxCommand(() => {

					if (!NewAccount)
						Settings.Accounts.Remove(Account);

					Settings.Accounts.Add(Account);
					Settings.Save();

					this.Close(this);
				});
			}
		}

		public ICommand DeleteCommand
		{
			get
			{
				return new MvxCommand(() => {

					if (Settings.Accounts.Contains(Account))
						Settings.Accounts.Remove(Account);

					Settings.Save();

					this.Close(this);
				});
			}
		}

	}

}