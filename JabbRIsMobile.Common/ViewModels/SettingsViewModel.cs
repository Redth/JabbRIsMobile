using System;
using Cirrious.MvvmCross.ViewModels;
using JabbRIsMobile.Common.Services;

namespace JabbRIsMobile.Common.ViewModels
{
	public class SettingsViewModel : MvxViewModel
	{
		public SettingsViewModel(ISettingsService settings)
		{
			_settings = settings;
		}

		ISettingsService _settings;




	}

}