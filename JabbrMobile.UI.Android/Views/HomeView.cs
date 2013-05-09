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
using Android.Content.PM;
using LegacyBar.Library.BarActions;

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "JabbR", MainLauncher=true, ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.KeyboardHidden, Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class HomeView : BaseFragmentView
	{
		SlidingMenu slidingMenu;
		MenuFragment menuFragment;
		UserListFragment userListFragment;
		ChatFragment chatFragment;
		HomeViewModel homeViewModel;

		protected override void OnViewModelSet ()
		{
			//this.Parent.RequestWindowFeature (WindowFeatures.NoTitle);

			//RequestWindowFeature (WindowFeatures.NoTitle);

			homeViewModel = (HomeViewModel)ViewModel;

			SetContentView (Resource.Layout.Content_Frame);

			MenuId = Resource.Menu.HomeMenu;


			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

			//LegacyBar.SetHomeLogo(Resource.Drawable.jabbr_home_icon);
			AddHomeAction (() => {
				slidingMenu.Toggle();
			}, Resource.Drawable.jabbr_home_icon);

			LegacyBar.Click += (sender, e) => {
				slidingMenu.Toggle();
			};
			LegacyBar.Title = "JabbR";

			var itemActionBarAction = new MenuItemLegacyBarAction(
				this, Resource.Id.menu_leave_room, Resource.Drawable.icon_exit, Resource.String.menu_string_exit)
			{
				ActionType = ActionType.Always,
			};

			LegacyBar.AddAction(itemActionBarAction);


			slidingMenu = new SlidingMenu (this) {
				Mode = MenuMode.LeftRight,
				TouchModeAbove = TouchMode.Fullscreen,
				BehindOffset = 80,
				ShadowWidth = 20,
				//ShadowDrawableRes = Resource.Drawable.SlidingMenuShadow,
				//SecondaryShadowDrawableRes = Resource.Drawable.SlidingMenuShadowRight
			};
		
			slidingMenu.AttachToActivity (this, SlideStyle.Content);
			slidingMenu.SetMenu (Resource.Layout.Menu_Frame);
			//slidingMenu.ShadowDrawableRes = Resource.Drawable.SlidingMenuShadow;

			menuFragment = new MenuFragment ();
			menuFragment.ViewModel = ViewModel;

			SupportFragmentManager.BeginTransaction ()
				.Replace (Resource.Id.menu_frame, menuFragment).Commit ();

			slidingMenu.SetSecondaryMenu (Resource.Layout.UserList_Frame);
			//slidingMenu.SecondaryShadowDrawableRes = Resource.Drawable.SlidingMenuShadowRight;
			userListFragment = new UserListFragment ();
			userListFragment.ViewModel = homeViewModel.CurrentRoom;

			SupportFragmentManager.BeginTransaction ()
				.Replace (Resource.Id.userlist_frame, userListFragment).Commit ();



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

					this.RunOnUiThread(() => LegacyBar.Title = homeViewModel.CurrentRoom.Room.Name);

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



		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.menu_leave_room:
					AlertDialog.Builder d;
					d = new AlertDialog.Builder (this);
					d.SetMessage ("Are you sure you want to leave: " + this.homeViewModel.CurrentRoom.Room.Name + "?");
					d.SetPositiveButton ("Yes", (o, e) => {

						this.homeViewModel.LeaveCurrentRoomCommand.Execute (null);
					
					});
					d.SetNegativeButton ("No", (o, e) => {

					});
					d.Show ();
				
					return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}

