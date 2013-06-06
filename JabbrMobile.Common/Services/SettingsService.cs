using System;
using System.Linq;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using System.Collections.ObjectModel;
using Cirrious.CrossCore;

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
			var fileStore = Mvx.Resolve<Cirrious.MvvmCross.Plugins.File.IMvxFileStore> ();

			var accountsList = Accounts.ToList ();

			var json = Newtonsoft.Json.JsonConvert.SerializeObject (accountsList);

			fileStore.WriteFile ("accts.json", json);
		}
	
		public void Load ()
		{
			var fileStore = Mvx.Resolve<Cirrious.MvvmCross.Plugins.File.IMvxFileStore> ();

			var json = string.Empty;

			Accounts.Clear ();

			if (fileStore.TryReadTextFile ("accts.json", out json))
			{
				var acctsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Account>> (json);

				foreach (var a in acctsList)
					Accounts.Add (a);
			}
		}

		public ObservableCollection<Account> Accounts { get;set; }
	}
	
}