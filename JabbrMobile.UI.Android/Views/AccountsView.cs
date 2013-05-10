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
	[Activity (Label = "Accounts", ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.KeyboardHidden|ConfigChanges.ScreenSize, Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class AccountsView : BaseView
	{
		AccountsViewModel viewModel;

		protected override void OnViewModelSet ()
		{
			viewModel = this.ViewModel as AccountsViewModel;

			SetContentView (Resource.Layout.View_Accounts);

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

			MenuId = Resource.Menu.AccountsMenu;


			AddHomeAction (() => {
				this.Finish();
			}, Resource.Drawable.jabbr_home_icon, true); 

			var itemActionBarAction = new MenuItemLegacyBarAction (
				this, Resource.Id.menu_add, Resource.Drawable.icon_add, Resource.String.menu_string_save) {
				ActionType = ActionType.Always
			};
			LegacyBar.AddAction(itemActionBarAction);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.menu_add:
					this.viewModel.AddCommand.Execute (null);
					return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}

