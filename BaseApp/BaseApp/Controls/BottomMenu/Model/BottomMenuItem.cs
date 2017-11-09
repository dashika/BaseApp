using BaseApp.Events;
using System;
using System.ComponentModel;

namespace BaseApp
{
	public class BottomMenuItem : INotifyPropertyChanged, IDisposable
	{
		protected string _activeImage;
		protected string _unActiveImage;

		private bool _isActive;

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

		public BottomMenuItem()
		{
			_isActive = false;
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
			IconMenuItem = _activeImage;
		}

		private void DoUnActiveItem()
		{
			_isActive = false;
			IconMenuItem = _unActiveImage;
		}

		public virtual void Invoke()
		{
			EventFirer.Instance.RaiseEvent("UnSelectMenuItem");
			DoActiveItem();
		}

		public void Dispose()
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
