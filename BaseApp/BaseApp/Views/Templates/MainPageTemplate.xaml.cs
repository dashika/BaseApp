using BaseApp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseApp.Views.Templates
{
	public partial class MainPageTemplate 
	{
		public MainPageTemplate()
		{
			InitializeComponent();
			PopupPanel.IsVisible = false;

			SetupTaps();
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == "ParentContentPage")
			{
				var page = ParentContentPage as BaseContentPage;
				if (page != null)
				{
					OnAppering();
					page.OnApperingView = OnAppering;
					page.OnDisappearingView += OnDisappearing;
				}
				else
				{
					OnDisappearing();
				}
			}
		}

		private void OnAppering()
		{
			SubscribeEvent();
		}

		private void OnDisappearing()
		{
			UnSubscribeEvent();
		}

		private void SetupTaps()
		{

		}

		private void SubscribeEvent()
		{
			EventFirer.Instance.Subscribe("SlideMenuOpen", OnSlideMenuOpenClick);
			EventFirer.Instance.Subscribe("SlideMenuClose", OnSlideMenuCloseClick);
		}

		private void UnSubscribeEvent()
		{
			EventFirer.Instance.UnSubscribe("SlideMenuOpen", OnSlideMenuOpenClick);
			EventFirer.Instance.UnSubscribe("SlideMenuClose", OnSlideMenuCloseClick);
		}

		private async void OnSlideMenuOpenClick(object sender, EventArgs e)
		{
			StackLayoutSlidingMenu.IsVisible = true;
			await StackLayoutSlidingMenu.TranslateTo(StackLayoutSlidingMenu.Width, 0, 150);
		}

		private async void OnSlideMenuCloseClick(object sender, EventArgs e)
		{
			await StackLayoutSlidingMenu.TranslateTo(StackLayoutSlidingMenu.TranslationX - StackLayoutSlidingMenu.Width, 0, 150);
			StackLayoutSlidingMenu.IsVisible = false;
		}

	}
}