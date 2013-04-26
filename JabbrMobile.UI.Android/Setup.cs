using Android.Content;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using JabbRIsMobile;


namespace JabbrMobile.Android
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}
		
		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}