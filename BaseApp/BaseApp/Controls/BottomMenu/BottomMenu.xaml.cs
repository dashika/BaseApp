
using BaseApp.ViewModels;
using System;
using Xamarin.Forms;

namespace BaseApp.Controls
{
	public partial class BottomMenu
	{
		public BottomMenu()
		{
			InitializeComponent();
			InitMenu();
		}

		void InitMenu()
		{
			var model = new BottomMenuViewModel();
			int column = 0;
			foreach (var itemMenu in model.MenuItems)
			{
				ContentLayout.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = GridLength.Star
				});
				var cell = new BottomMenuItemCell
				{
					BindingContext = itemMenu
				};
				ContentLayout.Children.Add(cell, column, 0);
				column++;
			}
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (BindingContext is BaseViewModel model)
			{
				model.BottomMenuControl = this;
				model.SetBottomMenu();
			}
		}
	}
}