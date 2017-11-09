using BaseApp.Services;
using BaseApp.Views;

namespace BaseApp
{
	public class SettingMenuItemBottom : BottomMenuItem
	{
		public SettingMenuItemBottom()
		{
			_activeImage = "cat_pirate.png";
			_unActiveImage = "cat_pirate_un.png";
			IconMenuItem = Navigator.Instance.CurrentTab.Equals(nameof(SettingView)) ? _activeImage : _unActiveImage;
		}

		public override async void Invoke()
		{
			base.Invoke();
			await Navigator.Instance.NavSettings();
		}
	}
}
