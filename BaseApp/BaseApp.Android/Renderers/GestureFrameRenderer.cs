using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using BaseApp.Controls;
using BaseApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(GestureFrame), typeof(GestureFrameRenderer))]

namespace BaseApp.Droid.Renderers
{
	public class GestureFrameRenderer : FrameRenderer
	{

		private readonly CustomGestureListener _listener;
		private readonly GestureDetector _detector;
		private long _tapedDownTime;
		private float _tapedX;
		private float _tapedY;
		private bool _resizeReady = false;
		private double _resize = 0;
		private bool ignore_action = false;

		public GestureFrameRenderer()
		{
			_listener = new CustomGestureListener();
			_detector = new GestureDetector(_listener);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
			{
				this.GenericMotion -= HandleGenericMotion;
				this.Touch -= HandleTouch;
				_listener.OnSwipeLeft -= HandleOnSwipeLeft;
				_listener.OnSwipeRight -= HandleOnSwipeRight;
				_listener.OnSwipeTop -= HandleOnSwipeTop;
				_listener.OnSwipeDown -= HandleOnSwipeDown;
			}

			if (e.OldElement == null)
			{
				this.GenericMotion += HandleGenericMotion;
				this.Touch += HandleTouch;
				_listener.OnSwipeLeft += HandleOnSwipeLeft;
				_listener.OnSwipeRight += HandleOnSwipeRight;
				_listener.OnSwipeTop += HandleOnSwipeTop;
				_listener.OnSwipeDown += HandleOnSwipeDown;
			}
		}

		private double DetectResizeMotion(TouchEventArgs e)
		{
			if (_resizeReady)
			{
				if (e.Event.PointerCount != 2)
				{
					return -1;
				}
				MotionEvent.PointerCoords p0 = new MotionEvent.PointerCoords();
				e.Event.GetPointerCoords(0, p0);
				Console.WriteLine(string.Format("X:{0}, Y:{1}", p0.X, p0.Y));
				MotionEvent.PointerCoords p1 = new MotionEvent.PointerCoords();
				e.Event.GetPointerCoords(1, p1);
				Console.WriteLine(string.Format("X:{0}, Y:{1}", p1.X, p1.Y));
				double resize = Math.Sqrt(Math.Abs(p0.X - p1.X) * Math.Abs(p0.X - p1.X) + Math.Abs(p0.Y - p1.Y) * Math.Abs(p0.Y - p1.Y));
				Console.WriteLine(string.Format("resize:{0}", resize));
				return resize;
			}
			return -1;
		}

		void HandleTouch(object sender, TouchEventArgs e)
		{
			bool cancelBase = false;
			if (e.Event.ActionMasked == MotionEventActions.Down)
			{
				_tapedDownTime = e.Event.EventTime;
				_tapedX = e.Event.RawX;
				_tapedY = e.Event.RawY;
				GestureFrame _gi = (GestureFrame)this.Element;
				_gi.OnDown(sender, e, e.Event.RawX, e.Event.RawY, e.Event.ActionIndex);
			}
			if (e.Event.ActionMasked == MotionEventActions.Pointer1Up)
			{
				GestureFrame _gi = (GestureFrame)this.Element;
				_gi.OnResizeDone(sender, e);
				_resizeReady = false;
				ignore_action = true;
			}
			if (e.Event.ActionMasked == MotionEventActions.Pointer1Down)
			{
				_resizeReady = true;
				ignore_action = false;
				if (_resizeReady)
				{
					MotionEvent.PointerCoords p0 = new MotionEvent.PointerCoords();
					MotionEvent.PointerCoords p1 = new MotionEvent.PointerCoords();
					e.Event.GetPointerCoords(0, p0);
					e.Event.GetPointerCoords(1, p1);
					_resize = Math.Sqrt(Math.Abs(p0.X - p1.X) * Math.Abs(p0.X - p1.X) + Math.Abs(p0.Y - p1.Y) * Math.Abs(p0.Y - p1.Y));
				}
			}
			else if (e.Event.ActionMasked == MotionEventActions.Up)
			{
				GestureFrame _gi = (GestureFrame)this.Element;
				_gi.OnUp(sender, e, e.Event.RawX, e.Event.RawY);
				if ((e.Event.EventTime - _tapedDownTime < 250)
				&& Math.Abs(_tapedX - e.Event.RawX) < 20 && Math.Abs(_tapedY - e.Event.RawY) < 20)
				{
					_gi.OnTapped(sender, e, e.Event.RawX, e.Event.RawY);
				}
				if (e.Event.PointerCount <= 1)
					ignore_action = false;
			}
			else if (e.Event.ActionMasked == MotionEventActions.Move)
			{
				GestureFrame _gi = (GestureFrame)this.Element;
				double rs = DetectResizeMotion(e);
				if (-1 != rs)
				{
					_gi.OnResize(sender, e, _resize, rs);
				}
				else if (!ignore_action)
				{
					float d = Forms.Context.ApplicationContext.Resources.DisplayMetrics.Density;

					float deltaX = _tapedX - e.Event.RawX;
					float deltaY = _tapedY - e.Event.RawY;

					cancelBase = _gi.OnMove(sender, e, deltaX / d, deltaY / d);
				}
			}
			_detector.OnTouchEvent(e.Event);
		}

		void HandleGenericMotion(object sender, GenericMotionEventArgs e)
		{
			_detector.OnTouchEvent(e.Event);
		}

		void HandleOnSwipeLeft(object sender, EventArgs e)
		{
			GestureFrame _gi = (GestureFrame)this.Element;
			_gi.OnSwipeLeft();
		}

		void HandleOnSwipeRight(object sender, EventArgs e)
		{
			GestureFrame _gi = (GestureFrame)this.Element;
			_gi.OnSwipeRight();
		}

		void HandleOnSwipeTop(object sender, EventArgs e)
		{
			GestureFrame _gi = (GestureFrame)this.Element;
			_gi.OnSwipeTop();
		}

		void HandleOnSwipeDown(object sender, EventArgs e)
		{
			GestureFrame _gi = (GestureFrame)this.Element;
			_gi.OnSwipeDown();
		}
	}
}