
using BaseApp.Services;

namespace BaseApp
{
	public class HomeMenuItem : BottomMenuItem
	{
		public HomeMenuItem()
		{
			_activeImage = "cat_walk.png";
			_unActiveImage = "cat_walk_un.png";
			IconMenuItem = Navigator.Instance.CurrentTab.Equals(nameof(MainPage)) ? _activeImage : _unActiveImage;
		}

		public override async void Invoke()
		{
			base.Invoke();
			await Navigator.Instance.NavHome();
		}
	}
}
