using Android.Content;
using Cirrious.MvvmCross.ViewModels;
using JabbRIsMobile.Common;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Droid.Platform;


namespace JabbRIsMobile.UI.Android
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