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

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "Account Details")]			
	public class EditAccountView : BaseView
	{
		protected override void OnViewModelSet ()
		{
			SetContentView (Resource.Layout.View_EditAccount);

			MenuId = Resource.Menu.EditAccountMenu;

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

			LegacyBar.Title = "Account";

			//AddHomeAction (() => { }, Resource.Drawable.ic_menu_left);


			var itemActionBarAction = new MenuItemLegacyBarAction(
				this, Resource.Id.menu_save, Resource.Drawable.ic_action_search, Resource.String.Hello)
			{
				ActionType = ActionType.Always
			};
			LegacyBar.AddAction(itemActionBarAction);

		}
	}
}

