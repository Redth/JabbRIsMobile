using System;
using System.Collections.Generic;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;
using JabbRIsMobile.Common.Models;

namespace JabbRIsMobile.Common.Services
{
	public interface ISettingsService
	{
		List<Account> Servers { get;set ; }

		void Save();
		void Load();
	}
	
}