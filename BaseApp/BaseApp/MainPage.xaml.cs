using BaseApp.Views;

namespace BaseApp
{
    public partial class MainPage 
	{
		public MainViewModel Model;

		public MainPage()
		{
			InitializeComponent();
		    Navigation.PushAsync(new SearchingPage());
			Model = BindingContext as MainViewModel;
			if (null == Model) return;
			Model.View = this;
		}
	}
}
