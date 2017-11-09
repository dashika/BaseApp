using BaseApp.Services;
using BaseApp.Views.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseApp
{
	public partial class App : Application
	{
		public App()
		{
			Current.Resources = new ResourceDictionary();
			Current.Resources.Add("MainPageTemplate", new ControlTemplate(typeof(MainPageTemplate)));
	
			MainPage = InitPageEvents(Navigator.Instance.Init<MainPage, MainViewModel>());
		}

		private NavigationPage InitPageEvents(NavigationPage mainPage)
		{
			mainPage.Popped += (sender, args) =>
			{
				var a = args;
			};
			mainPage.Pushed += (sender, args) =>
			{
				var a = args;
			};
			return mainPage;
		}

		public static async Task<bool?> CallHardwareBackPressed()
		{
			Func<Task<bool?>> backPressed = HardwareBackPressed;
			if (backPressed != null)
			{
				return await backPressed();
			}

			return true;
		}

		public static Func<Task<bool?>> HardwareBackPressed
		{
			private get;
			set;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
