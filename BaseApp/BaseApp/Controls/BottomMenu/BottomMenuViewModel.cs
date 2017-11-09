using System.Collections.Generic;

namespace BaseApp
{
	public class BottomMenuViewModel
	{
		public List<BottomMenuItem> MenuItems
		{
			get;
			set;
		}

		public BottomMenuViewModel()
		{
			Init();
		}

		private void Init()
		{
			MenuItems = new List<BottomMenuItem>();
			MenuItems.Add(new HomeMenuItem());
			MenuItems.Add(new SettingMenuItemBottom());
		}
	}
}
