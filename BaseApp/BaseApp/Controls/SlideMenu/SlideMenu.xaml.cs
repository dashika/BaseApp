using BaseApp.Events;
using System;
using Xamarin.Forms;

namespace BaseApp.Controls
{
	public partial class SlideMenu
	{
		public SlideMenu()
		{
			try
			{
				InitializeComponent();
				var model = new SlideMenuViewModel();
				MenuListView.ItemsSource = model.MenuItems;
				MenuListView.ItemTapped += OnItemTaped;
				CloseMenuFrame.SwipeLeft += SwipeLeftEvent;
			}
			catch (Exception ex)
			{
			}
		}

		private void SwipeLeftEvent(object sender, EventArgs e)
		{
			CloseMenu();
		}

		private void CloseMenu()
		{
			EventFirer.Instance.RaiseEvent("SlideMenuClose");
		}

		void OnItemTaped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as MenuItem;
			if (item == null)
			{
				return;
			}
			CloseMenu();
			((ListView)sender).SelectedItem = null;
			item.Invoke();
		}
	}
}