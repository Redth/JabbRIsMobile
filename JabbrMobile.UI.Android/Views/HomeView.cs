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
using LegacyBar.Library;
using LegacyBar.Library.Bar;

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "JabbR", MainLauncher=true)] //, Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class HomeView : MvxFragmentActivity
	{
		SlidingMenu slidingMenu;
		MenuFragment menuFragment;
		ChatFragment chatFragment;
		HomeViewModel homeViewModel;
		LegacyBar.Library.Bar.LegacyBar LegacyBar;

		protected override void OnViewModelSet ()
		{
			//this.Parent.RequestWindowFeature (WindowFeatures.NoTitle);

			//RequestWindowFeature (WindowFeatures.NoTitle);

			homeViewModel = (HomeViewModel)ViewModel;

			SetContentView (Resource.Layout.Content_Frame);

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

			if (LegacyBar != null)
				Console.WriteLine ("LEGACY BARRD!");
			LegacyBar.SetHomeLogo(Resource.Drawable.ic_menu_up);
			LegacyBar.Click += (sender, e) => {
				slidingMenu.Toggle();
			};
			LegacyBar.Title = "JabbR";
			
			//MenuId = Resource.Menu.mainmenu;



			slidingMenu = new SlidingMenu (this) {
				TouchModeAbove = TouchMode.Fullscreen,
				BehindOffset = 80
			};
		
			slidingMenu.AttachToActivity (this, SlideStyle.Content);
			slidingMenu.SetMenu (Resource.Layout.Menu_Frame);

			ActionBar.Title = "JabbR";


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



		/*public int MenuId { get; set; }

		public override bool OnPrepareOptionsMenu(IMenu menu)
		{
			if (LegacyBar == null)
				return base.OnPrepareOptionsMenu(menu);

			menu.Clear();
			MenuInflater.Inflate(MenuId, menu);

			for (var i = 0; i < menu.Size(); i++)
			{
				var menuItem = menu.GetItem(i);
				menuItem.SetVisible(!LegacyBar.MenuItemsToHide.Contains(menuItem.ItemId));
			}
			return base.OnPrepareOptionsMenu(menu);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			if (MenuId > 0)
				MenuInflater.Inflate(MenuId, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public void AddHomeAction(Type activity, int resId)
		{
			var homeIntent = new Intent(this, activity);
			homeIntent.AddFlags(ActivityFlags.ClearTop);
			homeIntent.AddFlags(ActivityFlags.NewTask);
			LegacyBar.SetHomeAction(new LegacyBar.Library.BarActions.DefaultLegacyBarAction(this, homeIntent, resId));
			LegacyBar.SetDisplayHomeAsUpEnabled(true);
		}

		public void AddHomeAction(Action action, int resId, bool isHomeAsUpEnabled = true)
		{
			LegacyBar.SetHomeAction(new LegacyBar.Library.BarActions.ActionLegacyBarAction(this, action, resId));
			LegacyBar.SetDisplayHomeAsUpEnabled(isHomeAsUpEnabled);
		}*/

	}
}

