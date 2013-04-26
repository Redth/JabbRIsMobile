using System;
using System.Collections.Generic;
using Cirrious.MvvmCross;
using JabbR.Client;
using JabbrMobile.Common.Models;
using System.Collections.ObjectModel;

namespace JabbrMobile.Common.Services
{
	public interface ISettingsService
	{
		ObservableCollection<Account> Accounts { get;set ; }

		void Save();
		void Load();
	}
	
}