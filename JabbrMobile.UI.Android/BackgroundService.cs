using System;
using Android.App;
using Android.Content;

namespace JabbrMobile
{
	[Service]
	public class BackgroundService : IntentService
	{
		public BackgroundService () : base("JabbR Service")
		{

		}

		protected override void OnHandleIntent (Intent intent)
		{

		}
	}
}

