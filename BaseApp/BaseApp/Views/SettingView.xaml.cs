using BaseApp.ViewModels;
using Xamarin.Forms;

namespace BaseApp.Views
{
	public partial class SettingView 
	{
		SettingsViewModel _model = null;

		public SettingView()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_model = BindingContext as SettingsViewModel;
			if (_model == null)
			{
				return;
			}
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}