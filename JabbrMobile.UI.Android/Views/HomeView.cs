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
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using JabbrMobile.Common.Messages;

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "JabbR", MainLauncher=true, ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.KeyboardHidden|ConfigChanges.ScreenSize, Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class HomeView : BaseFragmentView
	{
		MvxSubscriptionToken _mvxMsgTokUserSelected;
		IMvxMessenger messenger;

		SlidingMenu slidingMenu;
		MenuFragment menuFragment;
		UserListFragment userListFragment;
		ChatFragment chatFragment;
		EmptyFragment emptyFragment;
		HomeViewModel homeViewModel;

		bool showActions = false;

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







			slidingMenu = new SlidingMenu (this) {
				Mode = MenuMode.LeftRight,
				TouchModeAbove = TouchMode.Fullscreen,
				BehindOffset = 80,
				ShadowWidth = 20,
				ShadowDrawableRes = Resource.Drawable.SlidingMenuShadow,
				SecondaryShadowDrawableRes = Resource.Drawable.SlidingMenuShadowRight
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

			emptyFragment = new EmptyFragment ();
			emptyFragment.ViewModel = this.ViewModel;

			SupportFragmentManager.BeginTransaction ()
				.Replace (Resource.Id.content_frame, emptyFragment).Commit ();


			//TODO: Put some kind of default view in the chat fragment space

			homeViewModel.PropertyChanged += (sender, e) => {

				Console.WriteLine("PropertyChanged: "+  e.PropertyName);

				if (e.PropertyName == "CurrentRoom")
				{
					if (homeViewModel.CurrentRoom == null)
					{
						if (emptyFragment == null)
							emptyFragment = new EmptyFragment();

						chatFragment = null;

						SupportFragmentManager.BeginTransaction ()
							.Replace (Resource.Id.content_frame, emptyFragment).Commit ();

						LegacyBar.Title = "JabbR";

						showActions = false;
						ToggleActions();

						userListFragment.ViewModel = null;

						SupportFragmentManager.BeginTransaction ()
							.Replace (Resource.Id.userlist_frame, userListFragment).Commit ();
						return;
					}

					showActions = true;


					if (chatFragment == null)
					{
						chatFragment = new ChatFragment();
						chatFragment.ViewModel = homeViewModel.CurrentRoom;

						SupportFragmentManager.BeginTransaction()
							.Replace(Resource.Id.content_frame, chatFragment).Commit();
					}
					else
					{
						chatFragment.ViewModel = homeViewModel.CurrentRoom;
					}

					if (userListFragment == null)
					{
						userListFragment = new UserListFragment();
						userListFragment.ViewModel = homeViewModel.CurrentRoom;

						SupportFragmentManager.BeginTransaction ()
							.Replace (Resource.Id.userlist_frame, userListFragment).Commit ();

					}
					else
					{
						userListFragment.ViewModel = homeViewModel.CurrentRoom;
					}


					ToggleActions();

					slidingMenu.Toggle();

					this.RunOnUiThread(() => LegacyBar.Title = homeViewModel.CurrentRoom.Room.Name);
				}
			};

			messenger = Mvx.Resolve<IMvxMessenger> ();
			_mvxMsgTokUserSelected = messenger.SubscribeOnMainThread<UserSelectedMessage> (msg => {
				chatFragment.AppendText("@" + msg.User.Name);

				slidingMenu.ShowContent(true);
			});

		}

		void ToggleActions()
		{
			if (!showActions)
			{
				LegacyBar.RemoveActionAtMenuId (Resource.Id.menu_leave_room);
				LegacyBar.RemoveActionAtMenuId (Resource.Id.menu_connection);
				return;
			}

			if (LegacyBar.ActionCount <= 1)
			{
				var connectionActionBarAction = new MenuItemLegacyBarAction (
					this, Resource.Id.menu_leave_room, Resource.Drawable.icon_green_circle_small, Resource.String.menu_string_exit) {
					ActionType = ActionType.Always,
				};

				LegacyBar.AddAction (connectionActionBarAction);

				var itemActionBarAction = new MenuItemLegacyBarAction (
					this, Resource.Id.menu_leave_room, Resource.Drawable.icon_exit, Resource.String.menu_string_exit) {
					ActionType = ActionType.CollapseActionView,
				};

				LegacyBar.AddAction (itemActionBarAction);
			}
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
					d.SetPositiveButton ("No", (o, e) => { });
					d.SetNegativeButton ("Yes", (o, e) => this.homeViewModel.LeaveCurrentRoomCommand.Execute (null));
					d.Show ();				
					return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}

