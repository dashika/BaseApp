using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BaseApp.Controls
{
	public class ItemBarBase : ContentView
	{
		public Action OnClick;
		private ICommand OnClickCommand => new Command(OnClickTap);

		public ItemBarBase()
		{
			this.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = OnClickCommand,
				NumberOfTapsRequired = 1
			});
		}

		protected virtual void OnClickTap()
		{
			OnClick?.Invoke();
		}
	}
}
