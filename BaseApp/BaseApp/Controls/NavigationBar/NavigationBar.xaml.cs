
using BaseApp.Events;
using BaseApp.Services;
using BaseApp.ViewModels;
using System;

namespace BaseApp.Controls
{
	public partial class NavigationBar
	{
		public BaseViewModel Model;
		public Action OnBackClick;

		public NavigationBar()
		{
			InitializeComponent();
			SetupEvent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			Model = (BaseViewModel)BindingContext;
			if (Model != null)
			{
				Model.NavigationBarControl = this;
				Model.SetNavigationBar();
			}
		}

		public void ShowBackButton(bool show)
		{
			ImageBack.IsVisibleItem = show;
			ForceLayout();
		}

		public void ShowIntroLogo(bool show)
		{
			//ImageIntroLogo.IsVisible = show;
			ForceLayout();
		}

		public void ShowPageTitle(bool show)
		{
			//	LabelPageTitle.IsVisible = show;
			ForceLayout();
		}

		public void ShowSlideMenuButton(bool show)
		{
			ImageMenu.IsVisibleItem = show;
			ForceLayout();
		}

		public void SetupEvent()
		{
			ImageMenu.OnClick += TapGestureRecognizer_OnMenuTapped;
			ImageBack.OnClick += TapGestureRecognizer_OnBackTapped;
		}

		private void TapGestureRecognizer_OnMenuTapped()
		{
			EventFirer.Instance.RaiseEvent("SlideMenuOpen");
		}

		private async void TapGestureRecognizer_OnBackTapped()
		{
			OnBackClick?.Invoke();
			await Navigator.Instance.Pop();
		}
	}
}