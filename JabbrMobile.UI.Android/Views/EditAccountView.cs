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
	[Activity (Label = "Account Details", Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class EditAccountView : BaseFragmentView
	{
		protected override void OnViewModelSet ()
		{
			SetContentView (Resource.Layout.View_EditAccount);

			MenuId = Resource.Menu.EditAccountMenu;

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

			AddHomeAction (() => {
				this.Finish();
			}, Resource.Drawable.jabbr_home_icon);

			var itemActionBarAction = new MenuItemLegacyBarAction(
				this, Resource.Id.menu_save, Resource.Drawable.icon_save, Resource.String.Hello)
			{
				ActionType = ActionType.Always
			};
			LegacyBar.AddAction(itemActionBarAction);

		}
	}
}

