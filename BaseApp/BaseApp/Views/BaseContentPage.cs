using BaseApp.Controls;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BaseApp.Views
{
	public abstract class BaseContentPage : ContentPage
	{
		public Action OnApperingView;
		public Action OnDisappearingView;

		public BaseContentPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
		}

		public bool IsSubscribed(string message)
		{
			return null != Subscriptions && Subscriptions.Contains(message);
		}

		public void RemoveSubscription(string message)
		{
			Subscriptions?.Remove(message);
		}

		public void AddSubscription(string message)
		{
			if (null == Subscriptions) Subscriptions = new List<string>();
			Subscriptions.Add(message);
		}

		#region Fields
		
		public List<string> Subscriptions;
		public bool IsFirstView;

		#endregion

		#region Events

		protected override void OnAppearing()
		{
			base.OnAppearing();
			try
			{
				OnApperingView?.Invoke();
				NavigationPage.SetHasNavigationBar(this, false);
			}
			catch (Exception ex)
			{
			}
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			OnDisappearingView?.Invoke();
		}

		#endregion

		#region Properties

		public virtual BottomMenu VirtualLayoutBottomMenu { get; set; }
		public virtual SlideMenu VirtualLayoutSlideMenu { get; set; }

		#endregion

		public virtual void OnPopView()
		{
		}
	}
}
