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
using LegacyBar.Library.BarActions;

namespace JabbrMobile.Android.Views
{
	public class BaseView : MvxFragmentActivity
	{
		public LegacyBar.Library.Bar.LegacyBar LegacyBar { get; set; }
		public int MenuId { get; set; }

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
			LegacyBar.SetHomeAction(new DefaultLegacyBarAction(this, homeIntent, resId));
			LegacyBar.SetDisplayHomeAsUpEnabled(true);
		}

		public void AddHomeAction(Action action, int resId, bool isHomeAsUpEnabled = true)
		{
			LegacyBar.SetHomeAction(new ActionLegacyBarAction(this, action, resId));
			LegacyBar.SetDisplayHomeAsUpEnabled(isHomeAsUpEnabled);
		}

	}
}

