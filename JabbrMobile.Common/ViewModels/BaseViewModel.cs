using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross;
using Cirrious.MvvmCross.Droid;
using Cirrious.MvvmCross.Platform;
using JabbrMobile.Common.Services;
using JabbR.Client.Models;
using System.Collections.Generic;
using JabbrMobile.Common.Models;
using Cirrious.MvvmCross.Plugins.Messenger;
using JabbrMobile.Common.Messages;

namespace JabbrMobile.Common.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		public BaseViewModel(ISettingsService settings, IJabbrService service, IMvxMessenger messenger)
		{
			Settings = settings;
			Service = service;
			Messenger = messenger;

			Commands = new MvxCommandCollectionBuilder().BuildCollectionFor(this);
		}
	
		
		public IMvxCommandCollection Commands { get; private set; }

		public ISettingsService Settings { get; private set; }
		public IJabbrService Service { get; private set; }
		public IMvxMessenger Messenger { get; private set; }
	}

}