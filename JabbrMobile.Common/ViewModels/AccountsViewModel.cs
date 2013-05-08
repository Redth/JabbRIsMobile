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
using Cirrious.CrossCore;

namespace JabbrMobile.Common.ViewModels
{
	public class AccountsViewModel : BaseViewModel
	{
		public AccountsViewModel() : base()
		{
			Mvx.Trace ("Found Accounts: " + Settings.Accounts.Count());
		}

		public ObservableCollection<Account> Accounts 
		{
			get { return Settings.Accounts; }
		}

		public ICommand EditAccountCommand
		{
			get
			{
				return new MvxCommand<Account>(account => {
					var bundle = new MvxBundle();
					bundle.Data.Add("AccountId", account.Id);
					ShowViewModel<EditAccountViewModel>(bundle);
				});
			}
		}
	}

}