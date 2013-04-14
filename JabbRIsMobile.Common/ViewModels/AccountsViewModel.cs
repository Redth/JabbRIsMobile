using System;
using Cirrious.MvvmCross.ViewModels;
using JabbRIsMobile.Common.Services;

namespace JabbRIsMobile.Common.ViewModels
{
	public class AccountsViewModel : MvxViewModel
	{
		public AccountsViewModel(ISettingsService settings)
		{
			_settings = settings;
		}

		ISettingsService _settings;


	}
	
}