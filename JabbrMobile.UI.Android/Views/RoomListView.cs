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
	public class RoomListView : BaseView
	{
		protected override void OnViewModelSet ()
		{
			SetContentView (Resource.Layout.View_RoomList);

			MenuId = Resource.Menu.EditAccountMenu;

			LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);


			AddHomeAction (() => { this.Finish(); }, Resource.Drawable.jabbr_home_icon);
		
		}
	}
}

