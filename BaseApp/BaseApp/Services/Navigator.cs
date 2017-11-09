using BaseApp.ViewModels;
using BaseApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseApp.Services
{
	public class Navigator : INotifyPropertyChanged
	{
		public Page StartPage;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public async Task BackCount(int cnt)
		{
			for (var counter = 1; counter < cnt; counter++) //!!!
			{
				StartPage.Navigation.RemovePage(
					StartPage.Navigation.NavigationStack[StartPage.Navigation.NavigationStack.Count - 2]);
			}
			await Pop();
		}

		public async Task PushAndRemovePrevious(Page page)
		{
			Navigator.Instance.CurrentTab = nameof(page);
			FluffyOpenPage(CurrentTab);
			var count = StartPage.Navigation.NavigationStack.Count;

			//Find page in stack
			var removeIndex = -1;
			for (var i = 1; i < count; i++)
			{
				try
				{
					if (null != page.Title && page.Title.Equals(StartPage.Navigation.NavigationStack[i].Title))
					{
						removeIndex = i;
						break;
					}
				}
				catch (Exception ex)
				{
				}
			}

			await Push(page);

			if (removeIndex != -1)
			{
				ClosePrevious(count - removeIndex, 1);
			}
		}

		public async Task PopToHome()
		{
			FluffyOpenPage(CurrentTab);
			var count = StartPage.Navigation.NavigationStack.Count;

			for (var i = 1; i < count - 1; i++)
			{
				await Pop();
			}
		}

		public void ClosePrevious()
		{
			var count = StartPage.Navigation.NavigationStack.Count;
			StartPage.Navigation.RemovePage(StartPage.Navigation.NavigationStack[count - 2]);
		}

		public void ClosePrevious(int count, int notRemoveLast = 0)
		{
			var pagesCount = StartPage.Navigation.NavigationStack.Count;
			for (var i = 1; i <= count; i++)
			{
				var page = StartPage.Navigation.NavigationStack[pagesCount - count - notRemoveLast];
				var popPage = page as BaseContentPage;
				popPage?.OnPopView();
				page.BindingContext = null;
				StartPage.Navigation.RemovePage(page);
			}
		}

		public async Task NavHome()
		{
			if (CurrentTab == nameof(MainPage))
			{
				return;
			}
			CurrentTab = nameof(MainPage);
			FluffyOpenPage(CurrentTab);
			await Push(new MainPage(), new MainViewModel());
		}

		public static void FluffyOpenPage(string pageName)
		{
			Task.Run(() =>
			{
				var dd = new Dictionary<string, string>
				{
					{"name", pageName}
				};
			});
		}

		public string CurrentTab  = nameof(MainPage);
		private static volatile Navigator _instance;
		private Navigator()
		{
		}

		public static Navigator Instance
		{
			get
			{
				if (_instance == null)
				{
					
						if (_instance == null)
							_instance = new Navigator();
					
				}

				return _instance;
			}
		}

		public string CurrentPageOnScreen { get; set; }
		public string PreviousPageOnScreen { get; set; }

		#region Init

		public NavigationPage Init<TView, TViewModel>()
			where TView : Page
			where TViewModel : BaseViewModel
		{
			var page = (Page)Activator.CreateInstance(typeof(TView));
			var viewModel = (BaseViewModel)Activator.CreateInstance(typeof(TViewModel));
			StartPage = page;
			StartPage.BindingContext = viewModel;
			var navigationPage = new NavigationPage(StartPage);
			navigationPage.BarBackgroundColor = Color.FromHex("#00a79d");
			navigationPage.BarTextColor = Color.White;
			return navigationPage;
		}

		public NavigationPage Init<TView>()
			where TView : Page
		{
			var page = (Page)Activator.CreateInstance(typeof(TView));
			StartPage = page;
			StartPage.BindingContext = null;
			return new NavigationPage(StartPage);
		}

		public NavigationPage Init(Page page, BaseViewModel viewModel)
		{
			page.BindingContext = viewModel;
			StartPage = page;
			StartPage.BindingContext = viewModel;
			return new NavigationPage(StartPage);
		}

		#endregion


		#region Push

		public void InsertPageBefore(Page page, Page before)
		{
			StartPage.Navigation.InsertPageBefore(page, before);
		}

		public async Task Push(Page page)
		{
			Instance.CurrentTab = nameof(page);

			await StartPage.Navigation.PushAsync(page, false);
		}

		public async Task Push(Page page, BaseViewModel viewModel)
		{
			Instance.CurrentTab = page.GetType().Name;

			await StartPage.Navigation.PushAsync(CreatePage(page, viewModel), false);
		}

		public async Task Push<TView, TViewModel>()
			where TView : Page
			where TViewModel : BaseViewModel
		{

			await StartPage.Navigation.PushAsync(CreatePage<TView, TViewModel>(), false);
		}

		public async Task Push<TView>()
			where TView : Page
		{
			await StartPage.Navigation.PushAsync(CreatePage<TView>(), false);
		}

		#endregion Push

		#region Pop

		public async Task Pop()
		{
			Page page = null;
			if (Device.RuntimePlatform == Device.iOS)
			{
				page = await StartPage.Navigation.PopAsync();
			}
			else
			{
				page = await StartPage.Navigation.PopAsync(false);
			}
			if (page is BaseContentPage pageView)
			{
				pageView.OnPopView();
			}
		}

		public async Task PopToRoot()
		{
			await StartPage.Navigation.PopToRootAsync(false);
		}

		public void RemovePage(Page page)
		{
			try
			{
				if (StartPage.Navigation.NavigationStack.Contains(page))
				{
					StartPage.Navigation.RemovePage(page);
					var popPage = page as BaseContentPage;
					popPage?.OnPopView();
				}
			}
			catch (Exception ex)
			{
			}
		}

		#endregion Pop

		#region CreatePage

		public Page CreatePage(Page page, BaseViewModel viewModel)
		{
			viewModel.View = page;
			page.BindingContext = viewModel;
			return page;
		}

		public Page CreatePage<TView, TViewModel>()
			where TView : Page
			where TViewModel : BaseViewModel
		{
			var page = (Page)Activator.CreateInstance(typeof(TView));
			var viewModel = (BaseViewModel)Activator.CreateInstance(typeof(TViewModel));
			page.BindingContext = viewModel;
			return page;
		}

		public Page CreatePage<TView>()
			where TView : Page
		{
			var page = (Page)Activator.CreateInstance(typeof(TView));
			page.BindingContext = null;
			return page;
		}

		public Page CreatePage<TView>(BaseViewModel viewModel)
			where TView : Page
		{
			var page = (Page)Activator.CreateInstance(typeof(TView));
			page.BindingContext = viewModel;
			return page;
		}

		#endregion CreatePage

		public async Task NavSettings(int settingId = -1)
		{
			if (CurrentTab == nameof(SettingView))
			{
				return;
			}
			FluffyOpenPage("Settings");
			CurrentTab = nameof(SettingView);
			var page = new SettingView();
			var model = new SettingsViewModel
			{
				SelectedSettingId = settingId
			};
			await Push(page, model);
		}
	}
}
