using System;
using Xamarin.Forms;

namespace BaseApp.Controls
{
	public class GestureFrame : Frame
	{
		public GestureFrame() { }

		public event EventHandler SwipeDown;
		public event EventHandler SwipeTop;
		public event EventHandler SwipeLeft;
		public event EventHandler SwipeRight;
		public event EventHandler HTaped;
		public event EventHandler HDown;
		public event EventHandler HMove;
		public event EventHandler HResize;
		public event EventHandler HResizeDone;
		public event EventHandler HUp;
		public float RawX;
		public float RawY;
		public float DeltaX;
		public float DeltaY;
		public double ResizeOrigin;
		public double ResizeCurrent;
		public int PointerCount;

		public bool IsSwipe
		{
			get { return (bool)GetValue(isSwipeProperty); }
			set { SetValue(isSwipeProperty, value); }
		}

		public static readonly BindableProperty isSwipeProperty =
				BindableProperty.Create(nameof(IsSwipe), typeof(bool), typeof(GestureFrame), true);

		public void OnSwipeDown()
		{
			SwipeDown?.Invoke(this, null);
		}

		public void OnSwipeTop()
		{
			SwipeTop?.Invoke(this, null);
		}

		public void OnSwipeLeft()
		{
			SwipeLeft?.Invoke(this, null);
		}

		public void OnSwipeRight()
		{
			SwipeRight?.Invoke(this, null);
		}

		public void OnTapped(object sender, EventArgs e, float rawX, float rawY)
		{
			RawX = rawX;
			RawY = rawY;
			HTaped?.Invoke(this, e);
		}

		public bool OnMove(object sender, EventArgs e, float deltaX, float deltaY)
		{
			DeltaX = deltaX;
			DeltaY = deltaY;
			if (HMove != null)
			{
				HMove(this, e);
				return true;
			}
			return false;
		}
		public bool OnResize(object sender, EventArgs e, double origin, double current)
		{
			ResizeOrigin = origin;
			ResizeCurrent = current;
			if (HResize != null)
			{
				HResize(this, e);
				return true;
			}
			return false;
		}
		public bool OnResizeDone(object sender, EventArgs e)
		{
			if (HResizeDone != null)
			{
				HResizeDone(this, e);
				return true;
			}
			return false;
		}
		public void OnUp(object sender, EventArgs e, float x, float y)
		{
			RawX = x;
			RawY = y;

			HUp?.Invoke(this, e);
		}
		public void OnDown(object sender, EventArgs e, float x, float y, int pointCount)
		{
			DeltaX = x;
			DeltaY = y;
			HDown?.Invoke(this, e);
		}
	}
}
