using BaseApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BaseApp.ViewModels
{
	public abstract class BaseViewModel : IDisposable, INotifyPropertyChanged
	{
		private string _errorMessage;
		private ICommand _facebookCommand;
		private bool _isBusy = false;
		private bool _isShowModalWait;

		private bool _isShowProgress;
		private int _progressPercentValue; // 0 - 100
		private Color _progressColor;
		private Color _progressPercentColor;
		
		private string _pageTitle;
		private string _memoryInfo;
		private ICommand _twitterCommand;
		protected int _updateLevel = 0;

		public NavigationBar NavigationBarControl { get; set; }

		public BottomMenu BottomMenuControl { get; set; }

		public bool IsShowShareView { get; set; } = false;

		public bool IsModalView { get; set; }

		public bool IsNormalView => !IsModalView;

		public Page View { get; set; }


		public bool IsBusy
		{
			get { return _isBusy; }
			set { _isBusy = value; OnPropertyChanged("IsBusy"); }
		}

		public bool IsShowModalWait
		{
			get { return _isShowModalWait; }
			set { _isShowModalWait = value; OnPropertyChanged(nameof(IsShowModalWait)); }
		}
		public bool IsShowProgress
		{
			get { return _isShowProgress; }
			set { _isShowProgress = value; OnPropertyChanged(nameof(IsShowProgress)); }
		}
		public int ProgressPercentValue
		{
			get { return _progressPercentValue; }
			set
			{
				_progressPercentValue = value;
				OnPropertyChanged(nameof(ProgressPercentValue));
				OnPropertyChanged(nameof(ProgressPercent));

			}
		}

		protected BaseViewModel()
		{
			_progressPercentValue = 0;
			_isShowProgress = false;
			_progressColor = Color.FromHex("007D86");

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					_progressPercentColor = Color.White;
					break;
				case Device.Android:
					_progressPercentColor = Color.FromHex("007D86");
					break;
				case Device.WinPhone:
					_progressPercentColor = Color.FromHex("007D86");
					break;
				default:
					break;
			}
		}

		public virtual void SetNavigationBar()
		{

		}

		public virtual void SetBottomMenu()
		{

		}
		public string ProgressPercent
		{
			get
			{
				if (_progressPercentValue < 0 || _progressPercentValue > 100)
				{
					return "";
				}
				return $"{_progressPercentValue}%";
			}
		}
		public Color ProgressColor => _progressColor;
		public Color ProgressPercentColor => _progressPercentColor;


		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; OnPropertyChanged("ErrorMessage"); }
		}

		public bool IsFollowing { get; set; }

		public ICommand FacebookCommand
		{
			get
			{
				return _facebookCommand ?? (
					_facebookCommand = new Command(async () => {
						//await Navigator.Instance.PushModal
					}));
			}
		}

		public ICommand TwitterCommand
		{
			get
			{
				return _twitterCommand ??
					   (_twitterCommand =
						   new Command(async () => {
							 //  await Navigator.Instance.PushModal
						   }));
			}
		}

		public string PageTitle
		{
			get { return _pageTitle; }
			set
			{
				_pageTitle = value;
				OnPropertyChanged("PageTitle");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public virtual Task FollowUnfollow()
		{
			return null;
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,
				new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(List<string> propertyList)
		{
			if (PropertyChanged != null)
			{
				foreach (var propertyName in propertyList)
				{
					PropertyChanged(this,
						new PropertyChangedEventArgs(propertyName));
				}
			}
		}

		public virtual void UpdateContent()
		{
		}

		public virtual void Dispose()
		{

		}
	}
}
