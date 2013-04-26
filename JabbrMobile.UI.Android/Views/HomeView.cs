using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using SlidingMenuSharp;
using Cirrious.MvvmCross.Droid.Fragging;
using JabbrMobile.Android;

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "JabbR", MainLauncher=true)]			
	public class HomeView : MvxFragmentActivity
	{
		SlidingMenu slidingMenu;
		MenuFragment menuFragment;

		protected override void OnViewModelSet ()
		{
			SetContentView (Resource.Layout.Content_Frame);

			slidingMenu = new SlidingMenu (this) {
				TouchModeAbove = TouchMode.Fullscreen,
				BehindWidth = 300

			};
		
			slidingMenu.AttachToActivity (this, SlideStyle.Content);
			slidingMenu.SetMenu (Resource.Layout.Menu_Frame);


			menuFragment = new MenuFragment ();
			menuFragment.ViewModel = ViewModel;

			SupportFragmentManager.BeginTransaction ()
				.Replace (Resource.Id.menu_frame, menuFragment).Commit ();
		}

	
		public override void OnBackPressed ()
		{
			if (slidingMenu.IsMenuShowing)
				slidingMenu.ShowContent ();
			else
				base.OnBackPressed ();
		}

	}
}

