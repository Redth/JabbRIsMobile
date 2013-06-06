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
	public class EditAccountView : BaseView
	{
		EditAccountViewModel viewModel;

		protected override void OnViewModelSet ()
		{
			viewModel = ViewModel as EditAccountViewModel;

			SetContentView (Resource.Layout.View_EditAccount);

			MenuId = Resource.Menu.EditAccountMenu;

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

			AddHomeAction (() => {
				this.Finish();
			}, Resource.Drawable.jabbr_home_icon);

			if (!viewModel.NewAccount)
			{
				var deleteActionBarAction = new MenuItemLegacyBarAction(
					this, Resource.Id.menu_delete, Resource.Drawable.icon_delete, Resource.String.menu_string_delete)
				{
					ActionType = ActionType.Always
				};
				LegacyBar.AddAction(deleteActionBarAction);
			}

			var itemActionBarAction = new MenuItemLegacyBarAction(
				this, Resource.Id.menu_save, Resource.Drawable.icon_save, Resource.String.menu_string_save)
			{
				ActionType = ActionType.Always
			};
			LegacyBar.AddAction(itemActionBarAction);

			//LegacyBar.Title = viewModel.NewAccount ? "Add Account" : "Edit Account";

		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.menu_save:
					this.viewModel.SaveCommand.Execute (null);
					return true;
				case Resource.Id.menu_delete:
					AlertDialog.Builder d;
					d = new AlertDialog.Builder (this);
					d.SetMessage ("Are you sure you want to delete this account?");
					d.SetPositiveButton ("No", (o, e) => { });
					d.SetNegativeButton ("Yes", (o, e) => this.viewModel.DeleteCommand.Execute (null));
					d.Show ();
					return true;
				
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}

