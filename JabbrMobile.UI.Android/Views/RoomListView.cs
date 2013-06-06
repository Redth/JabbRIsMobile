using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using LegacyBar.Library.BarActions;
using Android.Content.PM;
using JabbrMobile.Common.ViewModels;

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "Account Details", ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.KeyboardHidden|ConfigChanges.ScreenSize, Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class RoomListView : BaseFragmentView
	{
		RoomListViewModel viewModel;

		protected async override void OnViewModelSet ()
		{
			viewModel = (RoomListViewModel)ViewModel;

			SetContentView (Resource.Layout.View_RoomList);

			MenuId = Resource.Menu.EditAccountMenu;

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);


			AddHomeAction (() => { this.Finish(); }, Resource.Drawable.jabbr_home_icon);
		

			var loadingBar = this.FindViewById<LoadingBar> (Resource.Id.loadingBar);

			viewModel.PropertyChanged += (sender, e) => {

				if (e.PropertyName.Equals("IsLoading"))
				{
					Console.WriteLine("LOADING: " + viewModel.IsLoading.ToString());

					this.RunOnUiThread(() => {
						if (viewModel.IsLoading)
							loadingBar.ShowBar("Loading Rooms...");
						else
							loadingBar.HideBar();
					});
				}
			};

			await viewModel.LoadRooms ();
		}
	}
}

