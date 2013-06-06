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
using Android.Util;
using Android.Animation;

namespace JabbrMobile.Android
{
	public class LoadingBar : LinearLayout
	{
		LoadingBarController controller;
		View flashbar;

		public LoadingBar(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Inflate (context);
		}

		public LoadingBar(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			Inflate (context);
		}

		void Inflate(Context context)
		{
			var inflater = LayoutInflater.FromContext (context);

			inflater.Inflate (Resource.Layout.LoadingBar, this);

			flashbar = this.FindViewById (Resource.Id.loadingBar);

			controller = new LoadingBarController (flashbar);
		}

		public void ShowBar(string message)
		{
			controller.ShowBar (message);
		}

		public void HideBar(bool immediate = false)
		{
			controller.HideBar (immediate);
		}

		public class LoadingBarController : AnimatorListenerAdapter
		{
			View barView;
			TextView messageView;
			ViewPropertyAnimator barAnimator;
		
			public LoadingBarController (View flashBarView)
			{
				barView = flashBarView;
				barAnimator = barView.Animate ();
				messageView = barView.FindViewById<TextView> (Resource.Id.loadingBarMessage);

				HideBar (true);
			}

			public void ShowBar (string message)
			{
				messageView.Text = message;

				barView.Visibility = ViewStates.Visible;

				barAnimator.Cancel();
				barAnimator.Alpha (1);
				barAnimator.SetDuration (300);
				barAnimator.SetListener (null);
			}
			public void HideBar (bool immediate = false)
			{
				if (immediate) 
				{
					barView.Visibility = ViewStates.Gone;
					barView.Alpha = 0;
				}
				else 
				{
					barAnimator.Cancel();
					barAnimator.Alpha (0);
					barAnimator.SetDuration (300); 
					barAnimator.SetListener (this);
				}
			}

			public override void OnAnimationEnd (Animator animation)
			{
				barView.Visibility = ViewStates.Gone;
			}
		}
	}
}

