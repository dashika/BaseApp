using BaseApp.Services;

namespace BaseApp
{
	public class HomeMenuItemSlide : MenuItem
	{
		public HomeMenuItemSlide()
		{
			_activeImage = "cat_walk.png";
			_unActiveImage = "cat_walk_un.png";
			IconMenuItem = _unActiveImage;
			Title = "Home";
		}

		public override async void Invoke()
		{
			base.Invoke();
			await Navigator.Instance.NavHome();
		}
	}
}
