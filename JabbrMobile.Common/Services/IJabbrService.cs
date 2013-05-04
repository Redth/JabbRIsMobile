using System;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using System.Collections.ObjectModel;

namespace JabbrMobile.Common.Services
{
	public interface IJabbrService
	{
		ObservableCollection<JabbrConnection> Connections { get; }
	
		void AddClient(Account account);

	}
	
}