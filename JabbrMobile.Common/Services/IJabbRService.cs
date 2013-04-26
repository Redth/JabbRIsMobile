using System;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using JabbrMobile.Common.Models;

namespace JabbrMobile.Common.Services
{
	public interface IJabbrService
	{
		IEnumerable<JabbrClientWrapper> Clients { get; }
	
		void AddClient(Account account);

	}
	
}