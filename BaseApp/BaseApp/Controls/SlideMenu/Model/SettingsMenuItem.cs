using BaseApp.Services;

namespace BaseApp
{
	public class SettingsMenuItem : MenuItem
	{
		public SettingsMenuItem()
		{
			_activeImage = "cat_pirate.png";
			_unActiveImage = "cat_pirate_un.png";
			IconMenuItem = _unActiveImage;
			Title = "Settings";
		}

		public override async void Invoke()
		{
			base.Invoke();
			await Navigator.Instance.NavSettings();
		}
	}
}
