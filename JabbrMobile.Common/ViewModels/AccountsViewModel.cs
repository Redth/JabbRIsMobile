using System;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;

namespace JabbrMobile.Common.ViewModels
{
	public class AccountsViewModel : MvxViewModel
	{
		public AccountsViewModel(ISettingsService settings)
		{
			_settings = settings;
		}

		ISettingsService _settings;


		public ISettingsService Settings 
		{ 
			get { return _settings; }
			set { _settings = value; }
		}
	}
	
}