using System;
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
	public class AccountsViewModel : BaseViewModel
	{
		public AccountsViewModel() : base()
		{

		}

		public ObservableCollection<Account> Accounts 
		{
			get { return Settings.Accounts; }
		}

		public ICommand EditAccountCommand
		{
			get
			{
				return new MvxCommand(() => {

				});
			}
		}
	}

}