using System;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using JabbRIsMobile.Common.Models;

namespace JabbRIsMobile.Common.Services
{
	public class SettingsService : ISettingsService
	{
		public void Save ()
		{

		}
	
		public void Load ()
		{

		}

		public List<Account> Servers { get;set; }
	}
	
}