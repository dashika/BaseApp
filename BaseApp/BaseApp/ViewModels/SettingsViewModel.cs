namespace BaseApp.ViewModels
{
	class SettingsViewModel : BaseViewModel
	{
		public int SelectedSettingId = -1;

		public SettingsViewModel()
		{
			PageTitle = "SETTINGS";
		}

		public override void SetNavigationBar()
		{
			NavigationBarControl.ShowBackButton(true);
			NavigationBarControl.ShowPageTitle(true);
			NavigationBarControl.ShowIntroLogo(false);
			NavigationBarControl.ShowSlideMenuButton(false);
		}
	}
}
