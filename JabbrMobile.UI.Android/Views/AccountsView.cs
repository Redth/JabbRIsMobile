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

namespace JabbrMobile.Android.Views
{
	[Activity (Label = "Accounts", ConfigurationChanges=ConfigChanges.Orientation|ConfigChanges.KeyboardHidden|ConfigChanges.ScreenSize, Theme="@android:style/Theme.Holo.Light.NoActionBar")]			
	public class AccountsView : BaseView
	{
		protected override void OnViewModelSet ()
		{
			SetContentView (Resource.Layout.View_Accounts);

			MenuId = Resource.Menu.AccountsMenu;


			/*AddHomeAction (() => {
				this.Finish();
			}, Resource.Drawable.jabbr_home_icon, false); 
			*/
			/*var itemActionBarAction = new MenuItemLegacyBarAction (
				this, Resource.Id.menu_add, Resource.Drawable.icon_add, Resource.String.Hello) {
				ActionType = ActionType.Always
			};
			LegacyBar.AddAction(itemActionBarAction); */
		}
	}
}

