using System;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using System.Collections.ObjectModel;

namespace JabbrMobile.Common.Services
{
	public class SettingsService : ISettingsService
	{
		public SettingsService()
		{
			Accounts = new ObservableCollection<Account> ();
		}

		public void Save ()
		{

		}
	
		public void Load ()
		{

		}

		public ObservableCollection<Account> Accounts { get;set; }
	}
	
}