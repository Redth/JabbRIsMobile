using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross;
using Cirrious.MvvmCross.Droid;
using Cirrious.MvvmCross.Platform;
using JabbrMobile.Common.Services;

namespace JabbrMobile.Common.ViewModels
{
	public class HomeViewModel : MvxViewModel
	{
		ISettingsService _settings;
		IJabbrService _service;

		public HomeViewModel(ISettingsService settings, IJabbrService service)
		{
			_settings = settings;
			_service = service;

			try
			{
				_service.AddClient(new JabbrMobile.Common.Models.Account()
				{
					AutoConnect = true,
					Url = "https://jabbr.net/",
					Username = "MoJabbr",
					Password = "mojabber"
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine (ex);
			}
		}



		public ISettingsService Settings 
		{ 
			get { return _settings; }
			set { _settings = value; }
		}

		public IJabbrService Service
		{
			get { return _service; }
			set { _service = value; }
		}
		 
	}

}