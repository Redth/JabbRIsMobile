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
using JabbrMobile.Common.ViewModels;

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "JabbR", MainLauncher=true)]			
	public class HomeView : MvxFragmentActivity
	{
		SlidingMenu slidingMenu;
		MenuFragment menuFragment;
		ChatFragment chatFragment;
		HomeViewModel homeViewModel;

		protected override void OnViewModelSet ()
		{
			homeViewModel = (HomeViewModel)ViewModel;

			SetContentView (Resource.Layout.Content_Frame);

			slidingMenu = new SlidingMenu (this) {
				TouchModeAbove = TouchMode.Fullscreen,
				BehindOffset = 80
			};
		
			slidingMenu.AttachToActivity (this, SlideStyle.Content);
			slidingMenu.SetMenu (Resource.Layout.Menu_Frame);


			menuFragment = new MenuFragment ();
			menuFragment.ViewModel = ViewModel;

			SupportFragmentManager.BeginTransaction ()
				.Replace (Resource.Id.menu_frame, menuFragment).Commit ();

			//TODO: Put some kind of default view in the chat fragment space

			homeViewModel.PropertyChanged += (sender, e) => {

				Console.WriteLine("PropertyChanged: "+  e.PropertyName);

				if (e.PropertyName == "CurrentRoom")
				{
					Console.WriteLine("Switching Rooms: " + homeViewModel.CurrentRoom.Room.Name);

					//switch chat fragment
					chatFragment = new ChatFragment();
					chatFragment.ViewModel = homeViewModel.CurrentRoom;

					SupportFragmentManager.BeginTransaction()
						.Replace(Resource.Id.content_frame, chatFragment).Commit();

					slidingMenu.Toggle();

					//TODO: switch users list fragment
				}
			};
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

