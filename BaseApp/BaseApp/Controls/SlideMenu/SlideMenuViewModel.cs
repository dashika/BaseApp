using System.Collections.Generic;

namespace BaseApp
{
	public class SlideMenuViewModel
	{
		public List<MenuItem> MenuItems
		{
			get;
			set;
		}

		public SlideMenuViewModel()
		{
			Init();
		}

		private void Init()
		{
			MenuItems = new List<MenuItem>
			{
				new HomeMenuItemSlide(),
				new SettingsMenuItem()
			};
		}
	}
}
