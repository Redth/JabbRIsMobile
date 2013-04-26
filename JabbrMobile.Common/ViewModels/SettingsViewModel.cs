using System;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common.Services;

namespace JabbrMobile.Common.ViewModels
{
	public class SettingsViewModel : MvxViewModel
	{
		public SettingsViewModel(ISettingsService settings)
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