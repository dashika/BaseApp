using BaseApp.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BaseApp
{
	public class MainViewModel : BaseViewModel
	{
		public new MainPage View;
		private ICommand _contestTapCommand;
		private double _displayWidth;
		
		public double DisplayWidth
		{
			get { return _displayWidth; }
			set
			{
				_displayWidth = value;
				OnPropertyChanged("DisplayWidth");
			}
		}

		public override void SetNavigationBar()
		{
			NavigationBarControl.ShowBackButton(false);
			NavigationBarControl.ShowPageTitle(false);
			NavigationBarControl.ShowIntroLogo(true);
			NavigationBarControl.ShowSlideMenuButton(true);
		}
		
	}
}