using BaseApp.Events;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BaseApp
{
	public class MenuItem : INotifyPropertyChanged, IDisposable
	{
		protected readonly Color _activeTextColor = Color.FromHex("#00A99D");
		protected readonly Color _unActiveTextColor = Color.Black;
		protected string _activeImage;
		protected string _unActiveImage;
		private bool _isActive;

		private string _title = "";
		public string Title
		{
			get
			{
				return _title;
			}
			protected set
			{
				_title = value;
				OnPropertyChanged("Title");
			}
		}

		private string _countValue = "";
		public string CountValue
		{
			get
			{
				return _countValue;
			}
			protected set
			{
				_countValue = value;
				OnPropertyChanged("CountValue");
			}
		}

		private Color _textColor;
		public Color TextColor
		{
			get
			{
				return _textColor;
			}
			set
			{
				_textColor = value;
				OnPropertyChanged("TextColor");
			}
		}

		private string _iconMenuItem;
		public string IconMenuItem
		{
			get
			{
				return _iconMenuItem;
			}
			set
			{
				_iconMenuItem = value;
				OnPropertyChanged("IconMenuItem");
			}
		}

		public MenuItem()
		{
			_textColor = _unActiveTextColor;
			_isActive = false;
			EventFirer.Instance.SubscribeAlways("UnSelectMenuItem", OnUnSelectMenuItem);
		}

		void OnUnSelectMenuItem(object sender, EventArgs e)
		{
			if (_isActive)
			{
				DoUnActiveItem();
			}
		}

		private void DoActiveItem()
		{
			_isActive = true;
			TextColor = _activeTextColor;
			IconMenuItem = _activeImage;
		}

		private void DoUnActiveItem()
		{
			_isActive = false;
			TextColor = _unActiveTextColor;
			IconMenuItem = _unActiveImage;
		}

		public virtual void Invoke()
		{
			EventFirer.Instance.RaiseEvent("UnSelectMenuItem");
			DoActiveItem();
		}


		public virtual void Dispose()
		{
			EventFirer.Instance.UnSubscribe("UnSelectMenuItem", OnUnSelectMenuItem);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
