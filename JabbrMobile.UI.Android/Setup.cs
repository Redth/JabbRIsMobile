using Android.Content;
using Cirrious.MvvmCross.ViewModels;
using JabbrMobile.Common;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using JabbRIsMobile;
using System.Collections.Generic;
using System.Reflection;
using Cirrious.MvvmCross.Plugins.Visibility;


namespace JabbrMobile
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

		public override void LoadPlugins (Cirrious.CrossCore.Plugins.IMvxPluginManager pluginManager)
		{
			pluginManager.EnsurePluginLoaded<PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.File.PluginLoader> ();
			base.LoadPlugins(pluginManager);
		}
	}
}