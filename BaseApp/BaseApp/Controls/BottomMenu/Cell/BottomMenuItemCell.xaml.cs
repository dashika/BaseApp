using System.Windows.Input;
using Xamarin.Forms;

namespace BaseApp
{
	public partial class BottomMenuItemCell
	{
		private ICommand _tapCommand;
		public ICommand TapCommand => _tapCommand ?? (_tapCommand = new Command(OnClickItem));

		public BottomMenuItemCell()
		{
			InitializeComponent();
			SetupTap();
		}

		private void SetupTap()
		{
			GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = TapCommand,
				NumberOfTapsRequired = 1
			});
		}

		void OnClickItem(object obj)
		{
			var item = BindingContext as BottomMenuItem;
			item?.Invoke();
		}
	}
}