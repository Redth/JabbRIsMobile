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
			var acctId = parameters.Data ["AccountId"];

			Account = Settings.Accounts.Where (a => a.Id.Equals(acctId)).FirstOrDefault ();
		
			if (Account == null)
			{
				NewAccount = true;
				Account = new Account ();
			}
		}

		public bool NewAccount { get;set; }
		public Account Account { get; set; }

	}

}